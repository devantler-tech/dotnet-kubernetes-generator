using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Job for use with kubectl create job.
/// </summary>
public class Job
{
  /// <summary>
  /// Gets or sets the metadata for the job.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the container image to run.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the CronJob name to create the job from (alternative to Image).
  /// </summary>
  public string? From { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public IList<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the arguments to pass to the command.
  /// </summary>
  public IList<string>? Args { get; init; }
}