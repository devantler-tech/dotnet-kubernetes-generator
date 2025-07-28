namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the job template specification for a CronJob.
/// </summary>
public class CronJobJobTemplateSpec
{
  /// <summary>
  /// Gets or sets the pod template for the job.
  /// </summary>
  public required CronJobPodTemplate Template { get; init; }
}