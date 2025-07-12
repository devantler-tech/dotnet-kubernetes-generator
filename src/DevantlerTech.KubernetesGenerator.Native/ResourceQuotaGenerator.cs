using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ResourceQuota objects using 'kubectl create quota' commands.
/// </summary>
public class ResourceQuotaGenerator : BaseNativeGenerator<V1ResourceQuota>
{
  static readonly string[] _defaultArgs = ["create", "quota"];

  /// <summary>
  /// Generates a resource quota using kubectl create quota command.
  /// </summary>
  /// <param name="model">The resource quota object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when resource quota name is not provided.</exception>
  public override async Task GenerateAsync(V1ResourceQuota model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create resource quota '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a resource quota from a V1ResourceQuota object.
  /// </summary>
  /// <param name="model">The V1ResourceQuota object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(V1ResourceQuota model)
  {
    var args = new List<string> { };

    // Require that a resource quota name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the resource quota name.");
    }
    args.Add(model.Metadata.Name);

    // Add hard limits if specified
    if (model.Spec?.Hard?.Count > 0)
    {
      var hardLimits = model.Spec.Hard.Select(kvp => $"{kvp.Key}={kvp.Value}");
      args.Add($"--hard={string.Join(",", hardLimits)}");
    }

    // Add scopes if specified
    if (model.Spec?.Scopes?.Count > 0)
    {
      args.Add($"--scopes={string.Join(",", model.Spec.Scopes)}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
