namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

/// <summary>
/// Represents the restart policy for all containers within the pod.
/// </summary>
public enum PodRestartPolicy
{
  /// <summary>
  /// Always restart the containers.
  /// </summary>
  Always,
  /// <summary>
  /// Restart the containers only on failure.
  /// </summary>
  OnFailure,
  /// <summary>
  /// Never restart the containers.
  /// </summary>
  Never
}
