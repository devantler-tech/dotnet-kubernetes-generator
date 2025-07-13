using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJob(string name, string schedule, string image)
{
  /// <summary>
  /// Gets or sets the metadata for the cronjob.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the cron schedule for the job.
  /// </summary>
  public required string Schedule { get; set; } = schedule;

  /// <summary>
  /// Gets or sets the container image to run.
  /// </summary>
  public required string Image { get; set; } = image;

  /// <summary>
  /// Gets the command to run in the container.
  /// </summary>
  public Collection<string> Command { get; } = [];

  /// <summary>
  /// Gets the arguments to pass to the command.
  /// </summary>
  public Collection<string> Args { get; } = [];

  /// <summary>
  /// Gets or sets the restart policy for the job.
  /// </summary>
  public string? RestartPolicy { get; set; }
}