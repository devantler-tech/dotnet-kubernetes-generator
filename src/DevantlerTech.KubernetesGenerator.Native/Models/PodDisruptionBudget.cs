namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes PodDisruptionBudget for use with kubectl create poddisruptionbudget.
/// </summary>
public class PodDisruptionBudget
{
  /// <summary>
  /// Gets or sets the API version of this PodDisruptionBudget.
  /// </summary>
  public string ApiVersion { get; set; } = "policy/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "PodDisruptionBudget";

  /// <summary>
  /// Gets or sets the metadata for the pod disruption budget.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the pod disruption budget.
  /// </summary>
  public required PodDisruptionBudgetSpec Spec { get; init; }
}