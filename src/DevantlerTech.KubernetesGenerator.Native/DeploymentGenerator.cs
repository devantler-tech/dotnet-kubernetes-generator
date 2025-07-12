using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Deployment objects using 'kubectl create deployment' commands.
/// </summary>
public class DeploymentGenerator : BaseNativeGenerator<V1Deployment>
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
  public override async Task GenerateAsync(V1Deployment model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create deployment '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a deployment from a V1Deployment object.
  /// </summary>
  /// <param name="model">The V1Deployment object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create deployment has limited functionality compared to full deployment specifications.
  /// Supported properties:
  /// - metadata.name (required)
  /// - metadata.namespace (optional)
  /// - spec.replicas (optional, defaults to 1)
  /// - spec.template.spec.containers[].image (required, at least one)
  /// - spec.template.spec.containers[0].ports[0].containerPort (optional)
  /// 
  /// Unsupported properties are ignored, including:
  /// - Complex pod specifications (volumes, env vars, resource limits, etc.)
  /// - Multiple ports per container
  /// - Container commands and args
  /// - Pod security context
  /// - Node selectors, tolerations, affinity
  /// - Deployment strategy
  /// - And many other advanced features
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Deployment model)
  {
    var args = new List<string>();

    // Require that a deployment name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the deployment name.");
    }
    args.Add(model.Metadata.Name);

    // Extract container images from the pod template
    var containers = model.Spec?.Template?.Spec?.Containers;
    if (containers == null || containers.Count == 0)
    {
      throw new KubernetesGeneratorException("The model.Spec.Template.Spec.Containers must contain at least one container with an image.");
    }

    var images = new List<string>();
    foreach (var container in containers)
    {
      if (string.IsNullOrEmpty(container.Image))
      {
        throw new KubernetesGeneratorException($"Container '{container.Name}' must have an image specified.");
      }
      images.Add(container.Image);
    }

    // Add all images
    foreach (var image in images)
    {
      args.Add($"--image={image}");
    }

    // Add replicas if specified and not default
    if (model.Spec?.Replicas != null && model.Spec.Replicas != 1)
    {
      args.Add($"--replicas={model.Spec.Replicas}");
    }

    // Add port from the first container if specified
    var firstContainer = containers[0];
    if (firstContainer.Ports?.Count > 0)
    {
      var firstPort = firstContainer.Ports[0];
      if (firstPort.ContainerPort > 0)
      {
        args.Add($"--port={firstPort.ContainerPort}");
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
