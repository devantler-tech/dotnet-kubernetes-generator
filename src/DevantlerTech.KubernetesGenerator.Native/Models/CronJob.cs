using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a CronJob for use with kubectl create cronjob commands.
/// </summary>
public class CronJob
{
  /// <summary>
  /// Gets or sets the metadata for the CronJob.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the cron schedule expression.
  /// </summary>
  public required string Schedule { get; set; }

  /// <summary>
  /// Gets or sets the container image.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the container command and arguments.
  /// </summary>
  public IList<string>? Command { get; init; }
}