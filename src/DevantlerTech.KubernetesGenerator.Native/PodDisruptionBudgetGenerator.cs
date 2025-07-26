using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PodDisruptionBudget objects using 'kubectl create poddisruptionbudget' commands.
/// </summary>
public class PodDisruptionBudgetGenerator : BaseNativeGenerator<PodDisruptionBudget>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(PodDisruptionBudget model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create pod disruption budget '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a pod disruption budget from a PodDisruptionBudget object.
  /// </summary>
  /// <param name="model">The PodDisruptionBudget object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(PodDisruptionBudget model)
  {
    CheckMinMaxAvailabilityConstraints(model, out bool hasMinAvailable, out bool hasMaxUnavailable);

    var args = new List<string>
    {
      model.Metadata.Name,
      $"--selector={model.Spec.Selector}"
    };

    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    if (hasMinAvailable)
    {
      args.Add($"--min-available={model.Spec.MinAvailable}");
    }

    if (hasMaxUnavailable)
    {
      args.Add($"--max-unavailable={model.Spec.MaxUnavailable}");
    }

    return args.AsReadOnly();
  }

  static void CheckMinMaxAvailabilityConstraints(PodDisruptionBudget model, out bool hasMinAvailable, out bool hasMaxUnavailable)
  {
    hasMinAvailable = !string.IsNullOrEmpty(model.Spec.MinAvailable);
    hasMaxUnavailable = !string.IsNullOrEmpty(model.Spec.MaxUnavailable);
    if (!hasMinAvailable && !hasMaxUnavailable)
      throw new KubernetesGeneratorException("Either MinAvailable or MaxUnavailable must be specified");

    if (hasMinAvailable && hasMaxUnavailable)
      throw new KubernetesGeneratorException("Cannot specify both MinAvailable and MaxUnavailable");
  }
}
