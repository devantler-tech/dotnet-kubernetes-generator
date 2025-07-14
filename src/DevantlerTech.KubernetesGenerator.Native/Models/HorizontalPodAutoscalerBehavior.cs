namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the behavior configuration for HorizontalPodAutoscaler scaling.
/// </summary>
public class HorizontalPodAutoscalerBehavior
{
  /// <summary>
  /// Gets or sets the scaling behavior for scaling up.
  /// </summary>
  public object? ScaleUp { get; set; }

  /// <summary>
  /// Gets or sets the scaling behavior for scaling down.
  /// </summary>
  public object? ScaleDown { get; set; }
}