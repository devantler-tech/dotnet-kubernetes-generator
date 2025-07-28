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
  /// Generates a ConfigMap using kubectl create configmap command.
  /// </summary>
  /// <param name="model">The ConfigMap object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when ConfigMap name is not provided.</exception>
  public override async Task GenerateAsync(ConfigMap model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    if (string.IsNullOrWhiteSpace(model.Metadata.Name))
    {
      throw new KubernetesGeneratorException("A non-empty ConfigMap name must be provided.");
    }

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create ConfigMap '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a ConfigMap from a ConfigMap object.
  /// </summary>
  /// <param name="model">The ConfigMap object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(ConfigMap model)
  {
    var args = new List<string>
    {
      // Require that a ConfigMap name is provided
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrWhiteSpace(model.Metadata.Namespace))
    {
      args.Add("--namespace");
      args.Add(model.Metadata.Namespace);
    }

    if (model.Data != null && model.Data.Count > 0)
    {
      // Add literal key-value pairs
      foreach (var kvp in model.Data)
      {
        args.Add("--from-literal");
        args.Add($"{kvp.Key}={kvp.Value}");
      }
    }

    // Add append hash if specified
    if (model.AppendHash)
    {
      args.Add("--append-hash");
    }

    return args.AsReadOnly();
  }


}
