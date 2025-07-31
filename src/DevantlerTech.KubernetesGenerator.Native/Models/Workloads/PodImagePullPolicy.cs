namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

/// <summary>
/// Specifies the Kubernetes image pull policy.
/// </summary>
public enum PodImagePullPolicy
{
  /// <summary>
  /// Always pull the image.
  /// </summary>
  Always,

  /// <summary>
  /// Never pull the image.
  /// </summary>
  Never,

  /// <summary>
  /// Pull the image only if it is not present.
  /// </summary>
  IfNotPresent
}
