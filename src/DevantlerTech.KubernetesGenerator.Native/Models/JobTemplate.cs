namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the job template for a CronJob.
/// </summary>
public class JobTemplate
{
  /// <summary>
  /// Gets or sets the pod template for the job template.
  /// </summary>
  public required PodTemplate Template { get; init; }
}