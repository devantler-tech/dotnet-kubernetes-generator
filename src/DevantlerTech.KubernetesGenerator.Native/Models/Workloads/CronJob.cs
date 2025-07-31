namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

/// <summary>
/// Represents a Kubernetes CronJob for use with kubectl create cronjob.
/// </summary>
public class CronJob
{
  /// <summary>
  /// Gets or sets the API version of this CronJob.
  /// </summary>
  public string ApiVersion { get; set; } = "batch/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "CronJob";

  /// <summary>
  /// Gets or sets the metadata for the cronjob.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the cronjob.
  /// </summary>
  public required CronJobSpec Spec { get; init; }
}
