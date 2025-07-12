using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PodDisruptionBudget objects using 'kubectl create poddisruptionbudget' commands.
/// </summary>
public class PodDisruptionBudgetGenerator : BaseNativeGenerator<V1PodDisruptionBudget>
{
  static readonly string[] _defaultArgs = ["create", "poddisruptionbudget"];

  /// <summary>
  /// Generates a pod disruption budget using kubectl create poddisruptionbudget command.
  /// </summary>
  /// <param name="model">The pod disruption budget object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are not provided.</exception>
  public override async Task GenerateAsync(V1PodDisruptionBudget model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create pod disruption budget '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a pod disruption budget from a V1PodDisruptionBudget object.
  /// </summary>
  /// <param name="model">The V1PodDisruptionBudget object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// kubectl create poddisruptionbudget requires a name and selector.
  /// Either min-available or max-unavailable can be specified, but not both.
  /// If both are specified, min-available takes precedence.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1PodDisruptionBudget model)
  {
    var args = new List<string> { };

    // Require that a pod disruption budget name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the pod disruption budget name.");
    }
    args.Add(model.Metadata.Name);

    // Require that a selector is provided
    if (model.Spec?.Selector?.MatchLabels == null || model.Spec.Selector.MatchLabels.Count == 0)
    {
      throw new KubernetesGeneratorException("The model.Spec.Selector.MatchLabels must be set for the pod disruption budget selector.");
    }

    // Build selector string from MatchLabels
    var selectorParts = model.Spec.Selector.MatchLabels.Select(kvp => $"{kvp.Key}={kvp.Value}");
    var selectorString = string.Join(",", selectorParts);
    args.Add($"--selector={selectorString}");

    // Add min-available or max-unavailable (min-available takes precedence if both are specified)
    if (model.Spec.MinAvailable != null)
    {
      args.Add($"--min-available={model.Spec.MinAvailable}");
    }
    else if (model.Spec.MaxUnavailable != null)
    {
      args.Add($"--max-unavailable={model.Spec.MaxUnavailable}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
