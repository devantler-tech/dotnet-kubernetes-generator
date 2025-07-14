namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a Job for use with kubectl create job.
/// </summary>
public class JobSpec
{
  /// <summary>
  /// Gets or sets the container image for the job.
  /// Required when not creating from a cronjob.
  /// </summary>
  public string? Image { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public IList<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the name of the CronJob to create this job from.
  /// Format: "cronjob/name"
  /// </summary>
  public string? From { get; set; }

  /// <summary>
  /// Gets or sets the restart policy for the job pods.
  /// </summary>
  public PodRestartPolicy? RestartPolicy { get; set; }
}