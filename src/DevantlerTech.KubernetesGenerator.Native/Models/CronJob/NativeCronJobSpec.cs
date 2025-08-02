using Cronos;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models.Job;

namespace DevantlerTech.KubernetesGenerator.Native.Models.CronJob;

/// <summary>
/// Represents the specification for a CronJob for use with kubectl create cronjob.
/// </summary>
public class NativeCronJobSpec
{
  /// <summary>
  /// Represents the schedule in cron format for the cronjob.
  /// </summary>
  string _schedule = string.Empty;

  /// <summary>
  /// Represents the schedule in cron format for the cronjob.
  /// </summary>
  public required string Schedule
  {
    get => _schedule;
    init
    {
      // Validate the schedule format
      _schedule = CronExpression.TryParse(value, out _)
        ? value
        : throw new KubernetesGeneratorException($"Invalid cron schedule format: '{value}'.");
    }
  }

  /// <summary>
  /// Gets or sets the job template for the cronjob.
  /// </summary>
  public required NativeJobTemplate JobTemplate { get; init; }
}
