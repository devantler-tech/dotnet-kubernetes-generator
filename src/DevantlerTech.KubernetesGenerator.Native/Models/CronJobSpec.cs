namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJobSpec
{
  /// <summary>
  /// Gets or sets the schedule in cron format for the cronjob.
  /// </summary>
  public required CronSchedule Schedule { get; set; }

  /// <summary>
  /// Gets or sets the job template for the cronjob.
  /// </summary>
  public CronJobJobTemplate? JobTemplate { get; set; }

  /// <summary>
  /// Gets or sets the container image for the cronjob.
  /// This is a convenience property that creates the jobTemplate structure automatically.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// This is a convenience property.
  /// </summary>
  public IList<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the restart policy for the job.
  /// This is a convenience property.
  /// Supported values: OnFailure, Never.
  /// </summary>
  public PodRestartPolicy? RestartPolicy { get; init; }
}