namespace DevantlerTech.KubernetesGenerator.Native.Models.StatefulSet;

/// <summary>
/// Represents the persistent volume claim retention policy for a StatefulSet.
/// </summary>
public class NativeStatefulSetPersistentVolumeClaimRetentionPolicy
{
  /// <summary>
  /// Gets or sets what happens to PVCs when the StatefulSet is deleted.
  /// </summary>
  public NativeStatefulSetPersistentVolumeClaimRetentionPolicyType? WhenDeleted { get; set; }

  /// <summary>
  /// Gets or sets what happens to PVCs when the StatefulSet is scaled down.
  /// </summary>
  public NativeStatefulSetPersistentVolumeClaimRetentionPolicyType? WhenScaled { get; set; }
}
