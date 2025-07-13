using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ConfigMap objects using 'kubectl create configmap' commands.
/// </summary>
public class ConfigMapGenerator : BaseNativeGenerator<ConfigMap>
{
  static readonly string[] _defaultArgs = ["create", "configmap"];

  /// <summary>
  /// Generates a configmap using kubectl create configmap command.
  /// </summary>
  /// <param name="model">The configmap object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  public override async Task GenerateAsync(ConfigMap model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create configmap '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a configmap from a ConfigMap object.
  /// </summary>
  /// <param name="model">The ConfigMap object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(ConfigMap model)
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

    // Add all data as literals
    foreach (var kvp in model.Data)
    {
      args.Add($"--from-literal={kvp.Key}={kvp.Value}");
    }

    return args.AsReadOnly();
  }
}
