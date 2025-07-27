namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes HorizontalPodAutoscaler for use with kubectl autoscale.
/// </summary>
public class HorizontalPodAutoscaler
{
  /// <summary>
  /// Gets or sets the API version of this HorizontalPodAutoscaler.
  /// </summary>
  public string ApiVersion { get; set; } = "autoscaling/v2";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "HorizontalPodAutoscaler";

  /// <summary>
  /// Gets or sets the metadata for the HorizontalPodAutoscaler.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the HorizontalPodAutoscaler.
  /// </summary>
  public required HorizontalPodAutoscalerSpec Spec { get; set; }
}