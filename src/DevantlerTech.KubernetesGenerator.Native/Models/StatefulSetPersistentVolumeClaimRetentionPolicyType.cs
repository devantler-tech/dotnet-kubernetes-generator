namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the retention policy type for persistent volume claims in a StatefulSet.
/// </summary>
public enum StatefulSetPersistentVolumeClaimRetentionPolicyType
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