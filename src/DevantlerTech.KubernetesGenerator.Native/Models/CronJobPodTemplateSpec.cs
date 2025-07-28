namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the pod template specification for a CronJob.
/// </summary>
public class CronJobPodTemplateSpec
{
  /// <summary>
  /// Gets or sets the containers in the pod.
  /// </summary>
  public required IList<PodContainer> Containers { get; init; }

  /// <summary>
  /// Gets or sets the restart policy for all containers within the pod.
  /// </summary>
  public PodRestartPolicy? RestartPolicy { get; set; }
}