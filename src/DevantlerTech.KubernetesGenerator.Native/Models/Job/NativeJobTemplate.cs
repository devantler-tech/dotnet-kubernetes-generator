using DevantlerTech.KubernetesGenerator.Native.Models.Pod;

namespace DevantlerTech.KubernetesGenerator.Native.Models.Job;

/// <summary>
/// Represents the job template for a CronJob.
/// </summary>
public class NativeJobTemplate
{
  /// <summary>
  /// Gets or sets the pod template for the job template.
  /// </summary>
  public required NativePodTemplate Template { get; init; }
}
