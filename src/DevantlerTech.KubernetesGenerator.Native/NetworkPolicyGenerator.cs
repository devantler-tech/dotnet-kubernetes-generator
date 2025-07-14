using System.Collections.ObjectModel;
using System.Text;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes NetworkPolicy objects using 'kubectl create -f' commands.
/// </summary>
public class NetworkPolicyGenerator : BaseNativeGenerator<NetworkPolicy>
{
  static readonly string[] _defaultArgs = ["create", "-f", "-"];

  /// <summary>
  /// Generates a network policy using kubectl create -f command with YAML input.
  /// </summary>
  /// <param name="model">The network policy object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when network policy name is not provided.</exception>
  public override async Task GenerateAsync(NetworkPolicy model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    // Generate YAML from the model
    string yaml = GenerateYamlFromModel(model);
    
    // Create a temporary file with the YAML content
    string tempFilePath = Path.Combine(Path.GetTempPath(), $"networkpolicy-{Guid.NewGuid()}.yaml");
    await File.WriteAllTextAsync(tempFilePath, yaml, cancellationToken).ConfigureAwait(false);

    try
    {
      // Use kubectl create -f with the temporary file
      var args = new ReadOnlyCollection<string>(["create", "-f", tempFilePath]);
      string errorMessage = $"Failed to create network policy '{model.Metadata.Name}' using kubectl";
      await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
    }
    finally
    {
      // Clean up the temporary file
      if (File.Exists(tempFilePath))
      {
        File.Delete(tempFilePath);
      }
    }
  }

  /// <summary>
  /// Generates YAML content from a NetworkPolicy model.
  /// </summary>
  /// <param name="model">The NetworkPolicy model.</param>
  /// <returns>The YAML representation of the network policy.</returns>
  static string GenerateYamlFromModel(NetworkPolicy model)
  {
    var sb = new StringBuilder();
    sb.AppendLine("apiVersion: networking.k8s.io/v1");
    sb.AppendLine("kind: NetworkPolicy");
    sb.AppendLine("metadata:");
    sb.AppendLine($"  name: {model.Metadata.Name}");
    
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      sb.AppendLine($"  namespace: {model.Metadata.Namespace}");
    }

    if (model.Metadata.Labels?.Count > 0)
    {
      sb.AppendLine("  labels:");
      foreach (var label in model.Metadata.Labels)
      {
        sb.AppendLine($"    {label.Key}: {label.Value}");
      }
    }

    if (model.Metadata.Annotations?.Count > 0)
    {
      sb.AppendLine("  annotations:");
      foreach (var annotation in model.Metadata.Annotations)
      {
        sb.AppendLine($"    {annotation.Key}: {annotation.Value}");
      }
    }

    sb.AppendLine("spec:");
    
    // Pod selector
    if (model.PodSelector != null)
    {
      sb.AppendLine("  podSelector:");
      AppendPodSelectorYaml(sb, model.PodSelector, "    ");
    }
    else
    {
      sb.AppendLine("  podSelector: {}");
    }

    // Policy types
    if (model.PolicyTypes?.Count > 0)
    {
      sb.AppendLine("  policyTypes:");
      foreach (var policyType in model.PolicyTypes)
      {
        sb.AppendLine($"  - {policyType}");
      }
    }

    // Ingress rules
    if (model.Ingress?.Count > 0)
    {
      sb.AppendLine("  ingress:");
      foreach (var ingressRule in model.Ingress)
      {
        sb.AppendLine("  - {{}}");
        AppendIngressRuleYaml(sb, ingressRule, "    ");
      }
    }

    // Egress rules
    if (model.Egress?.Count > 0)
    {
      sb.AppendLine("  egress:");
      foreach (var egressRule in model.Egress)
      {
        sb.AppendLine("  - {{}}");
        AppendEgressRuleYaml(sb, egressRule, "    ");
      }
    }

    return sb.ToString();
  }

  static void AppendPodSelectorYaml(StringBuilder sb, NetworkPolicyPodSelector podSelector, string indent)
  {
    if (podSelector.MatchLabels?.Count > 0)
    {
      sb.AppendLine($"{indent}matchLabels:");
      foreach (var label in podSelector.MatchLabels)
      {
        sb.AppendLine($"{indent}  {label.Key}: {label.Value}");
      }
    }

    if (podSelector.MatchExpressions?.Count > 0)
    {
      sb.AppendLine($"{indent}matchExpressions:");
      foreach (var expression in podSelector.MatchExpressions)
      {
        sb.AppendLine($"{indent}- key: {expression.Key}");
        sb.AppendLine($"{indent}  operator: {expression.Operator}");
        if (expression.Values?.Count > 0)
        {
          sb.AppendLine($"{indent}  values:");
          foreach (var value in expression.Values)
          {
            sb.AppendLine($"{indent}  - {value}");
          }
        }
      }
    }
  }

  static void AppendIngressRuleYaml(StringBuilder sb, NetworkPolicyIngressRule ingressRule, string indent)
  {
    if (ingressRule.Ports?.Count > 0)
    {
      sb.AppendLine($"{indent}ports:");
      foreach (var port in ingressRule.Ports)
      {
        sb.AppendLine($"{indent}- port: {port.Port}");
        if (!string.IsNullOrEmpty(port.Protocol))
        {
          sb.AppendLine($"{indent}  protocol: {port.Protocol}");
        }
        if (port.EndPort.HasValue)
        {
          sb.AppendLine($"{indent}  endPort: {port.EndPort}");
        }
      }
    }

    if (ingressRule.From?.Count > 0)
    {
      sb.AppendLine($"{indent}from:");
      foreach (var peer in ingressRule.From)
      {
        sb.AppendLine($"{indent}- {{}}");
        AppendPeerYaml(sb, peer, $"{indent}  ");
      }
    }
  }

  static void AppendEgressRuleYaml(StringBuilder sb, NetworkPolicyEgressRule egressRule, string indent)
  {
    if (egressRule.Ports?.Count > 0)
    {
      sb.AppendLine($"{indent}ports:");
      foreach (var port in egressRule.Ports)
      {
        sb.AppendLine($"{indent}- port: {port.Port}");
        if (!string.IsNullOrEmpty(port.Protocol))
        {
          sb.AppendLine($"{indent}  protocol: {port.Protocol}");
        }
        if (port.EndPort.HasValue)
        {
          sb.AppendLine($"{indent}  endPort: {port.EndPort}");
        }
      }
    }

    if (egressRule.To?.Count > 0)
    {
      sb.AppendLine($"{indent}to:");
      foreach (var peer in egressRule.To)
      {
        sb.AppendLine($"{indent}- {{}}");
        AppendPeerYaml(sb, peer, $"{indent}  ");
      }
    }
  }

  static void AppendPeerYaml(StringBuilder sb, NetworkPolicyPeer peer, string indent)
  {
    if (peer.PodSelector != null)
    {
      sb.AppendLine($"{indent}podSelector:");
      AppendPodSelectorYaml(sb, peer.PodSelector, $"{indent}  ");
    }

    if (peer.NamespaceSelector != null)
    {
      sb.AppendLine($"{indent}namespaceSelector:");
      AppendPodSelectorYaml(sb, peer.NamespaceSelector, $"{indent}  ");
    }

    if (peer.IPBlock != null)
    {
      sb.AppendLine($"{indent}ipBlock:");
      sb.AppendLine($"{indent}  cidr: {peer.IPBlock.CIDR}");
      if (peer.IPBlock.Except?.Count > 0)
      {
        sb.AppendLine($"{indent}  except:");
        foreach (var except in peer.IPBlock.Except)
        {
          sb.AppendLine($"{indent}  - {except}");
        }
      }
    }
  }
}
