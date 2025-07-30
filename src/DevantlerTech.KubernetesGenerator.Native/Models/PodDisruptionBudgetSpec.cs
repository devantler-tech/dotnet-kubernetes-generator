namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a Kubernetes PodDisruptionBudget for use with kubectl create poddisruptionbudget.
/// </summary>
public class PodDisruptionBudgetSpec
{
  /// <summary>
  /// Gets or sets the label selector for the pods that this budget applies to.
  /// This is required for kubectl create poddisruptionbudget.
  /// </summary>
  public required string Selector { get; set; }

  /// <summary>
  /// Gets or sets the minimum number or percentage of selected pods that must be available.
  /// Can be an integer or a percentage (e.g., "50%").
  /// Either MinAvailable or MaxUnavailable must be specified, but not both.
  /// </summary>
  public string? MinAvailable { get; set; }

  /// <summary>
  /// Gets or sets the maximum number or percentage of selected pods that can be unavailable.
  /// Can be an integer or a percentage (e.g., "50%").
  /// Either MinAvailable or MaxUnavailable must be specified, but not both.
  /// </summary>
  public string? MaxUnavailable { get; set; }
}
