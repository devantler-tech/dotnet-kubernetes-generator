namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the job template for a CronJob.
/// </summary>
public class CronJobJobTemplate
{
  /// <summary>
  /// Gets or sets the metadata for the job template.
  /// </summary>
  public Metadata? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the job template.
  /// </summary>
  public required CronJobJobTemplateSpec Spec { get; init; }
}