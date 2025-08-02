namespace DevantlerTech.KubernetesGenerator.Native.Models.Job;

/// <summary>
/// Represents the specification for a Job for use with kubectl create job.
/// </summary>
public class NativeJobSpec
{
  /// <summary>
  /// Gets or sets the container image for the job.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public IList<string>? Command { get; init; }
}
