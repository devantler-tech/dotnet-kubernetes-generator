namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a HorizontalPodAutoscaler for use with BaseKubernetesGenerator.
/// </summary>
public class HorizontalPodAutoscaler(string name)
{
  /// <summary>
  /// Gets or sets the API version of the HorizontalPodAutoscaler.
  /// </summary>
  public string ApiVersion { get; set; } = "autoscaling/v2";

  /// <summary>
  /// Gets or sets the kind of the HorizontalPodAutoscaler.
  /// </summary>
  public string Kind { get; set; } = "HorizontalPodAutoscaler";

  /// <summary>
  /// Gets or sets the metadata for the HorizontalPodAutoscaler.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the spec for the HorizontalPodAutoscaler.
  /// </summary>
  public HorizontalPodAutoscalerSpec? Spec { get; set; }
}