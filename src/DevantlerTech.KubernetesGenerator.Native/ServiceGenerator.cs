using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Service objects using 'kubectl create service' commands.
/// </summary>
public class ServiceGenerator : BaseNativeGenerator<V1Service>
{
  /// <summary>
  /// Generates a Service using kubectl create service command.
  /// </summary>
  /// <param name="model">The service object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when service name is not provided or service type is not supported.</exception>
  public override async Task GenerateAsync(V1Service model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. GetServiceTypeArgs(model), .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create service '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Gets the service type specific arguments for kubectl create service.
  /// </summary>
  /// <param name="model">The V1Service object.</param>
  /// <returns>The service type specific arguments.</returns>
  static ReadOnlyCollection<string> GetServiceTypeArgs(V1Service model)
  {
    string serviceType = model.Spec?.Type ?? "ClusterIP";

    return serviceType.ToUpperInvariant() switch
    {
      "CLUSTERIP" => new ReadOnlyCollection<string>(["create", "service", "clusterip"]),
      "NODEPORT" => new ReadOnlyCollection<string>(["create", "service", "nodeport"]),
      "LOADBALANCER" => new ReadOnlyCollection<string>(["create", "service", "loadbalancer"]),
      "EXTERNALNAME" => new ReadOnlyCollection<string>(["create", "service", "externalname"]),
      _ => throw new KubernetesGeneratorException($"Service type '{serviceType}' is not supported. Supported types are: ClusterIP, NodePort, LoadBalancer, ExternalName.")
    };
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a service from a V1Service object.
  /// </summary>
  /// <param name="model">The V1Service object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(V1Service model)
  {
    var args = new List<string>();

    // Require that a service name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the service name.");
    }
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Add service type specific options
    string serviceType = model.Spec?.Type ?? "ClusterIP";
    switch (serviceType.ToUpperInvariant())
    {
      case "CLUSTERIP":
        AddClusterIPOptions(args, model);
        break;
      case "NODEPORT":
        AddNodePortOptions(args, model);
        break;
      case "LOADBALANCER":
        AddLoadBalancerOptions(args, model);
        break;
      case "EXTERNALNAME":
        AddExternalNameOptions(args, model);
        break;
      default:
        throw new KubernetesGeneratorException($"Service type '{serviceType}' is not supported. Supported types are: ClusterIP, NodePort, LoadBalancer, ExternalName.");
    }

    return args.AsReadOnly();
  }

  /// <summary>
  /// Adds ClusterIP specific options to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  static void AddClusterIPOptions(List<string> args, V1Service model)
  {
    // Add ClusterIP if specified
    if (!string.IsNullOrEmpty(model.Spec?.ClusterIP))
    {
      args.Add($"--clusterip={model.Spec.ClusterIP}");
    }

    // Add TCP ports
    AddTcpPorts(args, model);
  }

  /// <summary>
  /// Adds NodePort specific options to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  static void AddNodePortOptions(List<string> args, V1Service model)
  {
    // Add NodePort if specified
    if (model.Spec?.Ports?.Count > 0)
    {
      var nodePortInfo = model.Spec.Ports.FirstOrDefault(p => p.NodePort.HasValue);
      if (nodePortInfo?.NodePort.HasValue == true)
      {
        args.Add($"--node-port={nodePortInfo.NodePort.Value}");
      }
    }

    // Add TCP ports
    AddTcpPorts(args, model);
  }

  /// <summary>
  /// Adds LoadBalancer specific options to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  static void AddLoadBalancerOptions(List<string> args, V1Service model) =>
    // Add TCP ports
    AddTcpPorts(args, model);

  /// <summary>
  /// Adds ExternalName specific options to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  static void AddExternalNameOptions(List<string> args, V1Service model)
  {
    // ExternalName is required for ExternalName services
    if (!string.IsNullOrEmpty(model.Spec?.ExternalName))
    {
      args.Add($"--external-name={model.Spec.ExternalName}");
    }
    else
    {
      throw new KubernetesGeneratorException("ExternalName service requires the ExternalName to be specified in the service spec.");
    }

    // Add TCP ports if specified
    AddTcpPorts(args, model);
  }

  /// <summary>
  /// Adds TCP port mappings to the arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Service object.</param>
  static void AddTcpPorts(List<string> args, V1Service model)
  {
    if (model.Spec?.Ports?.Count > 0)
    {
      foreach (var port in model.Spec.Ports)
      {
        if (port.Protocol?.ToUpperInvariant() == "TCP" || string.IsNullOrEmpty(port.Protocol))
        {
          // Format: --tcp=port:targetPort
          string tcpArg = $"--tcp={port.Port}";
          if (port.TargetPort != null)
          {
            tcpArg += $":{port.TargetPort.Value}";
          }
          args.Add(tcpArg);
        }
      }
    }
  }
}
