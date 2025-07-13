using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Docker registry Kubernetes Secret objects using 'kubectl create secret docker-registry' commands.
/// </summary>
public class DockerRegistrySecretGenerator : BaseNativeGenerator<DockerRegistrySecret>
{
  static readonly string[] _defaultArgs = ["create", "secret", "docker-registry"];
  /// <summary>
  /// Generates a Docker registry secret using kubectl create secret docker-registry command.
  /// </summary>
  /// <param name="model">The Docker registry secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(DockerRegistrySecret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create Docker registry secret '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a Docker registry secret from a DockerRegistrySecret object.
  /// </summary>
  /// <param name="model">The DockerRegistrySecret object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(DockerRegistrySecret model)
  {
    var args = new List<string> { };

    // Require that a secret name is provided
    if (string.IsNullOrEmpty(model.Metadata.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the secret name.");
    }
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add Docker registry specific arguments
    if (!string.IsNullOrEmpty(model.DockerServer))
    {
      args.Add($"--docker-server={model.DockerServer}");
    }

    args.Add($"--docker-username={model.DockerUsername}");
    args.Add($"--docker-password={model.DockerPassword}");
    args.Add($"--docker-email={model.DockerEmail}");

    return args.AsReadOnly();
  }
}
