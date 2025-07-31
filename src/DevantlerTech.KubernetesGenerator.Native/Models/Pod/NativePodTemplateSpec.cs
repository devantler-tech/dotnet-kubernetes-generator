namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents a pod template specification.
/// </summary>
public class NativePodTemplateSpec
{
  /// <summary>
  /// Gets or sets the metadata for the pod template.
  /// </summary>
  public NativeTemplateMetadata? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the pod template.
  /// </summary>
  public NativePodSpec? Spec { get; set; }
}
