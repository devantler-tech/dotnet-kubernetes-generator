namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a pod template specification.
/// </summary>
public class PodTemplateSpec
{
  /// <summary>
  /// Gets or sets the metadata for the pod template.
  /// </summary>
  public TemplateMetadata? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the pod template.
  /// </summary>
  public PodSpec? Spec { get; set; }
}
