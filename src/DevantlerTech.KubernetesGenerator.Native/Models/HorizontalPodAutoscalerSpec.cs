namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the spec for a HorizontalPodAutoscaler.
/// </summary>
public class HorizontalPodAutoscalerSpec
{
  /// <summary>
  /// Gets or sets the reference to the target object to scale.
  /// </summary>
  public ScaleTargetRef? ScaleTargetRef { get; set; }

  /// <summary>
  /// Gets or sets the minimum number of replicas.
  /// </summary>
  public int? MinReplicas { get; set; }

  /// <summary>
  /// Gets or sets the maximum number of replicas.
  /// </summary>
  public int MaxReplicas { get; set; }

  /// <summary>
  /// Gets or sets the list of metrics to use for scaling.
  /// </summary>
  public IList<HorizontalPodAutoscalerMetric>? Metrics { get; init; }

  /// <summary>
  /// Gets or sets the behavior configuration for scaling.
  /// </summary>
  public HorizontalPodAutoscalerBehavior? Behavior { get; set; }
}