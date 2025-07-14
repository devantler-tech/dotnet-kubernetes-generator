using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

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

    if (model.Images.Count == 0)
      throw new KubernetesGeneratorException("At least one image must be specified for the deployment.");

    // kubectl create deployment doesn't support multiple images with commands
    bool hasCommand = !string.IsNullOrEmpty(model.Command) || model.Args.Count > 0;
    if (hasCommand && model.Images.Count > 1)
      throw new KubernetesGeneratorException("Multiple images cannot be specified when using command arguments.");

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create deployment '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a deployment from a Deployment object.
  /// </summary>
  /// <param name="model">The Deployment object.</param>
  static ReadOnlyCollection<string> AddArguments(Deployment model)
  {
    var args = new List<string>
    {
      // The deployment name is always available from the metadata (required in constructor)
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add images (at least one is required)
    foreach (var image in model.Images)
    {
      args.Add($"--image={image}");
    }

    // Add replicas if specified
    if (model.Replicas.HasValue)
    {
      args.Add($"--replicas={model.Replicas.Value}");
    }

    // Add port if specified
    if (model.Port.HasValue)
    {
      args.Add($"--port={model.Port.Value}");
    }

    // Add command and arguments if specified
    if (!string.IsNullOrEmpty(model.Command) || model.Args.Count > 0)
    {
      args.Add("--");
      
      if (!string.IsNullOrEmpty(model.Command))
      {
        args.Add(model.Command);
      }
      
      args.AddRange(model.Args);
    }

    return args.AsReadOnly();
  }
}
