namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJob
{
  /// <summary>
  /// Gets or sets the metadata for the cronjob.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Gets or sets the cron schedule for the job.
  /// </summary>
  public required string Schedule { get; set; }

  /// <summary>
  /// Gets or sets the container image to run.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public List<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the arguments to pass to the command.
  /// </summary>
  public List<string>? Args { get; init; }

  /// <summary>
  /// Gets or sets the restart policy for the job.
  /// </summary>
  public string? RestartPolicy { get; set; }
}