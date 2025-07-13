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
  public HorizontalPodAutoscalerMetadata? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the scale target reference.
  /// </summary>
  public required ScaleTargetRef ScaleTargetRef { get; set; }

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

/// <summary>
/// Represents the metadata for a HorizontalPodAutoscaler.
/// </summary>
public class HorizontalPodAutoscalerMetadata
{
  /// <summary>
  /// Gets or sets the name of the HorizontalPodAutoscaler.
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the HorizontalPodAutoscaler.
  /// </summary>
  public string? Namespace { get; set; }
}

/// <summary>
/// Represents the scale target reference for a HorizontalPodAutoscaler.
/// </summary>
public class ScaleTargetRef
{
  /// <summary>
  /// Gets or sets the API version of the target resource.
  /// </summary>
  public string? ApiVersion { get; set; }

  /// <summary>
  /// Gets or sets the kind of the target resource.
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the target resource.
  /// </summary>
  public required string Name { get; set; }
}