namespace DevantlerTech.KubernetesGenerator.Native.Models.Job;

/// <summary>
/// Represents a Kubernetes Job for use with kubectl create job.
/// </summary>
public class NativeJob
{
  /// <summary>
  /// Gets or sets the API version of this Job.
  /// </summary>
  public string ApiVersion { get; set; } = "batch/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "Job";

  /// <summary>
  /// Gets or sets the metadata for the job.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the job.
  /// </summary>
  public required NativeJobSpec Spec { get; init; }
}
