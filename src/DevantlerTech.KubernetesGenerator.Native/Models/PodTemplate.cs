namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a pod template for job specifications.
/// </summary>
public class PodTemplate
{
  /// <summary>
  /// Gets or sets the specification for the pod template.
  /// </summary>
  public required PodSpec Spec { get; init; }
}
