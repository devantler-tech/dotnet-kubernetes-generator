namespace DevantlerTech.KubernetesGenerator.Native.Models.Autoscaling;

/// <summary>
/// Represents the specification for a HorizontalPodAutoscaler using kubectl autoscale.
/// </summary>
public class HorizontalPodAutoscalerSpec
{
  /// <summary>
  /// Gets or sets the target resource reference.
  /// </summary>
  public required HorizontalPodAutoscalerScaleTargetRef ScaleTargetRef { get; set; }

  /// <summary>
  /// Gets or sets the minimum number of replicas.
  /// If null or negative, kubectl will apply a default value.
  /// </summary>
  public int? MinReplicas { get; set; }

  /// <summary>
  /// Gets or sets the maximum number of replicas. This is required.
  /// </summary>
  public required int MaxReplicas { get; set; }
}
