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
  public override async Task GenerateAsync(KubernetesDeployment model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create deployment '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a deployment from a KubernetesDeployment object.
  /// </summary>
  /// <param name="model">The KubernetesDeployment object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(KubernetesDeployment model)
  {
    var args = new List<string>
    {
      model.Metadata.Name,
      $"--image={model.Image}"
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
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

    // Add environment variables
    foreach (var envVar in model.Environment)
    {
      args.Add($"--env={envVar.Key}={envVar.Value}");
    }

    return args.AsReadOnly();
  }
}
