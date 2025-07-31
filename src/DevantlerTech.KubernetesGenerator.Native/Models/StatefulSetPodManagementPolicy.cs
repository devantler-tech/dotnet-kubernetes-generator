namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the pod management policy for a StatefulSet.
/// </summary>
public enum StatefulSetPodManagementPolicy
{
  /// <summary>
  /// OrderedReady pod management policy (default) - pods are created in order and wait for ready.
  /// </summary>
  OrderedReady,

  /// <summary>
  /// Parallel pod management policy - pods are created in parallel.
  /// </summary>
  Parallel
}
