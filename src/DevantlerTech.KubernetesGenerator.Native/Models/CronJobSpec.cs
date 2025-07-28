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
  public required JobTemplate JobTemplate { get; init; }
}