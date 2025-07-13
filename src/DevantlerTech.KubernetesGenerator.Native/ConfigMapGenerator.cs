using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ConfigMap objects using 'kubectl create configmap' commands.
/// </summary>
public class ConfigMapGenerator : BaseNativeGenerator<ConfigMap>
{
  static readonly string[] _defaultArgs = ["create", "configmap"];

  /// <summary>
  /// Generates a ConfigMap using kubectl create configmap command.
  /// </summary>
  /// <param name="model">The ConfigMap object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  public override async Task GenerateAsync(ConfigMap model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create ConfigMap '{model.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a ConfigMap from a ConfigMap object.
  /// </summary>
  /// <param name="model">The ConfigMap object.</param>
  static ReadOnlyCollection<string> AddOptions(ConfigMap model)
  {
    List<string> args = [];

    // Add the ConfigMap name (required)
    args.Add(model.Name);

    // Add data from Data property as literals
    if (model.Data?.Count > 0)
    {
      foreach (var kvp in model.Data)
      {
        args.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Namespace))
    {
      args.Add($"--namespace={model.Namespace}");
    }

    return args.AsReadOnly();
  }
}
