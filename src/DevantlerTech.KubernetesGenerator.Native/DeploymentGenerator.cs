using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Deployment objects using 'kubectl create deployment' commands.
/// </summary>
public class DeploymentGenerator : BaseNativeGenerator<Deployment>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when deployment name is not provided or no images are specified.</exception>
  public override async Task GenerateAsync(Deployment model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    if (string.IsNullOrWhiteSpace(model.Metadata.Name))
    {
      throw new KubernetesGeneratorException("A non-empty Deployment name must be provided.");
    }

    if (model.Spec.Images.Count == 0)
    {
      throw new KubernetesGeneratorException("At least one container image must be provided.");
    }

    if (model.Spec.Images.Count > 1 && model.Spec.Command != null && model.Spec.Command.Count > 0)
    {
      throw new KubernetesGeneratorException("Cannot specify multiple images with a command. kubectl create deployment does not support this combination.");
    }

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create deployment '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a deployment from a Deployment object.
  /// </summary>
  /// <param name="model">The Deployment object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(Deployment model)
  {
    var args = new List<string>
    {
      // Require that a deployment name is provided
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrWhiteSpace(model.Metadata.Namespace))
    {
      args.Add("--namespace");
      args.Add(model.Metadata.Namespace);
    }

    // Add container images
    foreach (string image in model.Spec.Images)
    {
      args.Add("--image");
      args.Add(image);
    }

    // Add replicas if specified
    if (model.Spec.Replicas.HasValue)
    {
      args.Add("--replicas");
      args.Add(model.Spec.Replicas.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    // Add port if specified
    if (model.Spec.Port.HasValue)
    {
      args.Add("--port");
      args.Add(model.Spec.Port.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    // Add command if specified
    if (model.Spec.Command != null && model.Spec.Command.Count > 0)
    {
      args.Add("--");
      args.AddRange(model.Spec.Command);
    }

    return args.AsReadOnly();
  }
}
