using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes LoadBalancer Service objects using 'kubectl create service loadbalancer' commands.
/// </summary>
public class LoadBalancerServiceGenerator : BaseNativeGenerator<V1Service>
{
  static readonly string[] _defaultArgs = ["create", "service", "loadbalancer"];

  /// <summary>
  /// Generates a LoadBalancer Service using kubectl create service loadbalancer command.
  /// </summary>
  /// <param name="model">The service object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when service name is not provided.</exception>
  public override async Task GenerateAsync(V1Service model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create LoadBalancer service '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a LoadBalancer service from a V1Service object.
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

    // Add TCP ports
    AddTcpPorts(args, model);

    return args.AsReadOnly();
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