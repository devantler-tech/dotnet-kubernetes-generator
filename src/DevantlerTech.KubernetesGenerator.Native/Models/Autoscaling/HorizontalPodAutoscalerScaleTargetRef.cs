namespace DevantlerTech.KubernetesGenerator.Native.Models.Autoscaling;

/// <summary>
/// Represents the target resource reference for HorizontalPodAutoscaler.
/// </summary>
public class HorizontalPodAutoscalerScaleTargetRef
{
  /// <summary>
  /// Gets or sets the API version of the target resource.
  /// </summary>
  public string ApiVersion { get; set; } = "apps/v1";

  /// <summary>
  /// Gets or sets the kind of the target resource.
  /// </summary>
  public required HorizontalPodAutoscalerTargetKind Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the target resource.
  /// </summary>
  public required string Name { get; set; }
}
