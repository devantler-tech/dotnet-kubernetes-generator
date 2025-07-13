using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Ingress objects using 'kubectl create ingress' commands.
/// </summary>
public class IngressGenerator : BaseNativeGenerator<Ingress>
{
  static readonly string[] _defaultArgs = ["create", "ingress"];

  /// <summary>
  /// Generates an ingress using kubectl create ingress command.
  /// </summary>
  /// <param name="model">The ingress object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when ingress name is not provided.</exception>
  public override async Task GenerateAsync(Ingress model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create ingress '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating an ingress from an Ingress object.
  /// </summary>
  /// <param name="model">The Ingress object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create ingress supports basic ingress creation with name, class, rules, default backend, and annotations.
  /// Advanced properties like TLS configuration beyond basic rule-based TLS are limited to what kubectl create supports.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(Ingress model)
  {
    var args = new List<string>();

    // Require that an ingress name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the ingress name.");
    }
    args.Add(model.Metadata.Name);

    // Add ingress class if specified
    if (!string.IsNullOrEmpty(model.IngressClassName))
    {
      args.Add($"--class={model.IngressClassName}");
    }

    // Add default backend if specified
    if (model.DefaultBackend != null)
    {
      args.Add($"--default-backend={model.DefaultBackend.ServiceName}:{model.DefaultBackend.ServicePort}");
    }

    // Add rules if specified
    if (model.Rules != null)
    {
      foreach (var rule in model.Rules)
      {
        string host = rule.Host ?? "";
        string path = rule.Path ?? "/";
        string serviceName = rule.Backend.ServiceName;
        string servicePort = rule.Backend.ServicePort;

        // Build the rule string: host/path=service:port[,tls=secret]
        string ruleString = $"{host}{path}={serviceName}:{servicePort}";

        // Add TLS if specified
        if (!string.IsNullOrEmpty(rule.TlsSecretName))
        {
          ruleString += $",tls={rule.TlsSecretName}";
        }

        args.Add($"--rule={ruleString}");
      }
    }

    // Add annotations if specified
    if (model.Metadata?.Annotations != null)
    {
      foreach (var annotation in model.Metadata.Annotations)
      {
        args.Add($"--annotation={annotation.Key}={annotation.Value}");
      }
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
