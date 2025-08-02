namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents a pod template for job specifications.
/// </summary>
public class NativePodTemplate
{
  /// <summary>
  /// Gets or sets the specification for the pod template.
  /// </summary>
  public required NativePodSpec Spec { get; init; }
}
