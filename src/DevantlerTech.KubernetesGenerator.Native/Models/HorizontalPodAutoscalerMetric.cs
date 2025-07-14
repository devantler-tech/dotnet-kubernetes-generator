namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a metric for HorizontalPodAutoscaler scaling.
/// </summary>
public class HorizontalPodAutoscalerMetric
{
  /// <summary>
  /// Gets or sets the type of the metric.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Gets or sets the resource metric specification.
  /// </summary>
  public object? Resource { get; set; }

  /// <summary>
  /// Gets or sets the pods metric specification.
  /// </summary>
  public object? Pods { get; set; }

  /// <summary>
  /// Gets or sets the object metric specification.
  /// </summary>
  public object? ObjectMetric { get; set; }

  /// <summary>
  /// Gets or sets the external metric specification.
  /// </summary>
  public object? External { get; set; }

  /// <summary>
  /// Gets or sets the container resource metric specification.
  /// </summary>
  public object? ContainerResource { get; set; }
}