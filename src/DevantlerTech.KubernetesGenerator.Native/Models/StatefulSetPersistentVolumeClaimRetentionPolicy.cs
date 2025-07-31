namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the persistent volume claim retention policy for a StatefulSet.
/// </summary>
public class StatefulSetPersistentVolumeClaimRetentionPolicy
{
  /// <summary>
  /// Gets or sets what happens to PVCs when the StatefulSet is deleted.
  /// </summary>
  public StatefulSetPersistentVolumeClaimRetentionPolicyType? WhenDeleted { get; set; }

  /// <summary>
  /// Gets or sets what happens to PVCs when the StatefulSet is scaled down.
  /// </summary>
  public StatefulSetPersistentVolumeClaimRetentionPolicyType? WhenScaled { get; set; }
}
