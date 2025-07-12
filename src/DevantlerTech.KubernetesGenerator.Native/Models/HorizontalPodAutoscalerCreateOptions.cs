using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the options for creating a HorizontalPodAutoscaler using kubectl autoscale.
/// This model is limited to the capabilities of kubectl autoscale command.
/// </summary>
public class HorizontalPodAutoscalerCreateOptions
{
  /// <summary>
  /// Gets or sets the metadata for the HorizontalPodAutoscaler.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the scale target reference.
  /// </summary>
  public required V2CrossVersionObjectReference ScaleTargetRef { get; set; }

  /// <summary>
  /// Gets or sets the minimum number of pods that can be set by the autoscaler.
  /// If not specified, defaults to 1.
  /// </summary>
  public int? MinReplicas { get; set; }

  /// <summary>
  /// Gets or sets the maximum number of pods that can be set by the autoscaler.
  /// This is required.
  /// </summary>
  public required int MaxReplicas { get; set; }

  /// <summary>
  /// Gets or sets the target average CPU utilization (represented as a percent of requested CPU) over all the pods.
  /// If not specified, a default autoscaling policy will be used.
  /// </summary>
  public int? TargetCPUUtilizationPercentage { get; set; }
}