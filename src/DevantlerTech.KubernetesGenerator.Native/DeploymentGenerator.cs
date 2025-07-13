using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Deployment objects using 'kubectl create deployment' commands.
/// </summary>
public class DeploymentGenerator : BaseNativeGenerator<KubernetesDeployment>
{
  static readonly string[] _defaultArgs = ["create", "deployment"];

  /// <summary>
  /// Generates a deployment using kubectl create deployment command.
  /// </summary>
  /// <param name="model">The deployment object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when deployment name is not provided or no container images are specified.</exception>
  public override async Task GenerateAsync(KubernetesDeployment model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create deployment '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a deployment from a KubernetesDeployment object.
  /// </summary>
  /// <param name="model">The KubernetesDeployment object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create deployment has limited functionality compared to full deployment specifications.
  /// Supported properties:
  /// - metadata.name (required)
  /// - metadata.namespace (optional)
  /// - replicas (optional, defaults to 1)
  /// - images (required, at least one)
  /// - port (optional, from first container)
  /// 
  /// Unsupported properties include:
  /// - Complex pod specifications (volumes, env vars, resource limits, etc.)
  /// - Multiple ports per container
  /// - Container commands and args
  /// - Pod security context
  /// - Node selectors, tolerations, affinity
  /// - Deployment strategy
  /// - And many other advanced features
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(KubernetesDeployment model)
  {
    List<string> args = [];

    // Require that a deployment name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the deployment name.");
    }
    args.Add(model.Metadata.Name);

    // Require at least one container image
    if (model.Images.Count == 0)
    {
      throw new KubernetesGeneratorException("The model.Images must contain at least one image.");
    }

    // Validate that all images are not null or empty
    foreach (string image in model.Images)
    {
      if (string.IsNullOrEmpty(image))
      {
        throw new KubernetesGeneratorException("All images in model.Images must be non-null and non-empty.");
      }
    }

    // Add all images
    foreach (string image in model.Images)
    {
      args.Add($"--image={image}");
    }

    // Add replicas if specified and not default
    if (model.Replicas.HasValue && model.Replicas.Value != 1)
    {
      args.Add($"--replicas={model.Replicas.Value}");
    }

    // Add port if specified
    if (model.Port.HasValue && model.Port.Value > 0)
    {
      args.Add($"--port={model.Port.Value}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
