using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Job objects using 'kubectl create job' commands.
/// </summary>
public class JobGenerator : BaseNativeGenerator<V1Job>
{
  static readonly string[] _defaultArgs = ["create", "job"];

  /// <summary>
  /// Generates a job using kubectl create job command.
  /// </summary>
  /// <param name="model">The job object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when job name is not provided or when no image is specified.</exception>
  public override async Task GenerateAsync(V1Job model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create job '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a job from a V1Job object.
  /// </summary>
  /// <param name="model">The V1Job object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create job supports basic job creation with image and command.
  /// Advanced properties like parallelism, completions, and complex pod specifications are not supported
  /// by the kubectl create command and will be ignored.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Job model)
  {
    var args = new List<string>();

    // Require that a job name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the job name.");
    }
    args.Add(model.Metadata.Name);

    // Get the first container - we need an image
    var container = model.Spec?.Template?.Spec?.Containers?.FirstOrDefault();
    if (container == null || string.IsNullOrEmpty(container.Image))
    {
      throw new KubernetesGeneratorException("An image must be specified in the first container for job creation.");
    }

    args.Add($"--image={container.Image}");

    // Add command and args if specified
    if (container.Command?.Count > 0)
    {
      args.Add("--");
      args.AddRange(container.Command);

      // Add args if present
      if (container.Args?.Count > 0)
      {
        args.AddRange(container.Args);
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
