using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ConfigMap objects using 'kubectl create configmap' commands.
/// </summary>
public class ConfigMapGenerator : BaseNativeGenerator<V1ConfigMap>
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
  public override async Task GenerateAsync(V1ConfigMap model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create ConfigMap '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a ConfigMap from a V1ConfigMap object.
  /// </summary>
  /// <param name="model">The V1ConfigMap object.</param>
  static ReadOnlyCollection<string> AddOptions(V1ConfigMap model)
  {
    var args = new List<string>();

    // Require that a ConfigMap name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the ConfigMap name.");
    }
    args.Add(model.Metadata.Name);

    // Validate that kubectl create configmap doesn't support certain properties
    if (model.BinaryData?.Count > 0)
    {
      throw new KubernetesGeneratorException("The kubectl create configmap command does not support binaryData. Use BaseKubernetesGenerator instead for full ConfigMap support.");
    }

    if (model.Immutable.HasValue)
    {
      throw new KubernetesGeneratorException("The kubectl create configmap command does not support the immutable property. Use BaseKubernetesGenerator instead for full ConfigMap support.");
    }

    // Add data from Data property as literals
    if (model.Data?.Count > 0)
    {
      foreach (var kvp in model.Data)
      {
        args.Add($"--from-literal={kvp.Key}={kvp.Value}");
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
