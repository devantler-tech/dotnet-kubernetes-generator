using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterIP Service objects using 'kubectl create service clusterip' commands.
/// </summary>
public class ClusterIPServiceGenerator : ServiceGeneratorBase
{
  static readonly string[] _defaultArgs = ["create", "service", "clusterip"];

  /// <summary>
  /// Generates a ClusterIP Service using kubectl create service clusterip command.
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
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create ClusterIP service '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a ClusterIP service from a V1Service object.
  /// </summary>
  /// <param name="model">The V1Service object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(V1Service model)
  {
    var args = new Collection<string>();

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

    // Add ClusterIP if specified
    if (!string.IsNullOrEmpty(model.Spec?.ClusterIP))
    {
      args.Add($"--clusterip={model.Spec.ClusterIP}");
    }

    // Add TCP ports
    AddTcpPorts(args, model);

    return args.AsReadOnly();
  }
}
