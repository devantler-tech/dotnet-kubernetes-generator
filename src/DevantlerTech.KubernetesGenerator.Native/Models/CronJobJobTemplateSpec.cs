namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a pod template for job specifications.
/// </summary>
public class PodTemplate
{
  /// <summary>
  /// Gets or sets the pod template for the job.
  /// </summary>
  public required CronJobPodTemplate Template { get; init; }
}