namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Specifies the Kubernetes image pull policy.
/// </summary>
public enum NativePodImagePullPolicy
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
