using Cronos;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJobSpec
{
  string _schedule = string.Empty;

  /// <summary>
  /// Gets or sets the schedule in cron format for the cronjob.
  /// Accepts both string and CronExpression values with validation.
  /// </summary>
  public required string Schedule
  {
    get => _schedule;
    set
    {
      // Validate the cron expression using CronSchedule utility
      CronSchedule.Validate(value);
      _schedule = value;
    }
  }

  /// <summary>
  /// Sets the schedule from a CronExpression.
  /// </summary>
  /// <param name="cronExpression">The CronExpression to set.</param>
  public void SetSchedule(CronExpression cronExpression)
  {
    ArgumentNullException.ThrowIfNull(cronExpression);
    _schedule = CronSchedule.FromCronExpression(cronExpression);
  }

  /// <summary>
  /// Gets or sets the job template for the cronjob.
  /// </summary>
  public required JobTemplate JobTemplate { get; init; }
}