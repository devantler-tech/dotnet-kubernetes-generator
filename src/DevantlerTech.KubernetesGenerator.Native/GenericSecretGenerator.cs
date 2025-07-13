using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for generic Kubernetes Secret objects using 'kubectl create secret generic' commands.
/// </summary>
public class GenericSecretGenerator : BaseNativeGenerator<GenericSecret>
{
  static readonly string[] _defaultArgs = ["create", "secret", "generic"];
  /// <summary>
  /// Generates a generic secret using kubectl create secret generic command.
  /// </summary>
  /// <param name="model">The secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when secret name is not provided.</exception>
  public override async Task GenerateAsync(GenericSecret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create generic secret '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a generic secret from a GenericSecret object.
  /// </summary>
  /// <param name="model">The GenericSecret object.</param>
  static ReadOnlyCollection<string> AddOptions(GenericSecret model)
  {
    var args = new List<string>
    {
      // The secret name is always available from the metadata (required in constructor)
      model.Metadata.Name
    };

    // Add type if specified (but don't require it)
    if (!string.IsNullOrEmpty(model.Type))
    {
      args.Add($"--type={model.Type}");
    }

    // Add all data as literals
    foreach (var kvp in model.Data)
    {
      args.Add($"--from-literal={kvp.Key}={kvp.Value}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    return args.AsReadOnly();
  }
}
