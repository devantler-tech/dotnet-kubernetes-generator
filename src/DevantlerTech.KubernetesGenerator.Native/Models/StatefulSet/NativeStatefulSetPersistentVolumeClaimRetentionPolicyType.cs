namespace DevantlerTech.KubernetesGenerator.Native.Models.StatefulSet;

/// <summary>
/// Represents the retention policy type for persistent volume claims in a StatefulSet.
/// </summary>
public enum NativeStatefulSetPersistentVolumeClaimRetentionPolicyType
{
  /// <summary>
  /// Retain the PVCs.
  /// </summary>
  Retain,

  /// <summary>
  /// Delete the PVCs.
  /// </summary>
  Delete
}
