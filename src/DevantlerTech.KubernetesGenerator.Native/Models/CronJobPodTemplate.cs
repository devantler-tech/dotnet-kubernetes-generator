namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the pod template for a CronJob.
/// </summary>
public class CronJobPodTemplate
{
  /// <summary>
  /// Gets or sets the metadata for the pod template.
  /// </summary>
  public Metadata? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the pod template.
  /// </summary>
  public required CronJobPodTemplateSpec Spec { get; init; }
}