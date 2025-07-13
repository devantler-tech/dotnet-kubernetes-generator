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
  public override async Task GenerateAsync(GenericSecret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create generic secret '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a generic secret from a GenericSecret object.
  /// </summary>
  /// <param name="model">The GenericSecret object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(GenericSecret model)
  {
    var args = new List<string>
    {
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add type if specified
    if (!string.IsNullOrEmpty(model.Type))
    {
      args.Add($"--type={model.Type}");
    }

    // Add data from files
    if (model.FromFiles?.Count > 0)
    {
      foreach (string file in model.FromFiles)
      {
        args.Add($"--from-file={file}");
      }
    }

    // Add data from environment files
    if (model.FromEnvFiles?.Count > 0)
    {
      foreach (string envFile in model.FromEnvFiles)
      {
        args.Add($"--from-env-file={envFile}");
      }
    }

    // Add literal data
    if (model.FromLiterals?.Count > 0)
    {
      foreach (var kvp in model.FromLiterals)
      {
        args.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }

    // Add data as literals if no other sources are specified
    if (model.Data?.Count > 0 && model.FromFiles?.Count == 0 && model.FromEnvFiles?.Count == 0 && model.FromLiterals?.Count == 0)
    {
      foreach (var kvp in model.Data)
      {
        args.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }

    return args.AsReadOnly();
  }
}
