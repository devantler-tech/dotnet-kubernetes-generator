namespace DevantlerTech.KubernetesGenerator.Native.Models.StatefulSet;

/// <summary>
/// Represents the specification for a StatefulSet.
/// </summary>
public class NativeStatefulSetSpec
{
  /// <summary>
  /// Gets or sets the number of desired replicas. Defaults to 1 if not specified.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the label selector for pods. Existing ReplicaSets whose pods are matching this selector will be managed by this StatefulSet.
  /// </summary>
  public required NativeLabelSelector Selector { get; set; }

  /// <summary>
  /// Gets or sets the name of the service that governs this StatefulSet.
  /// </summary>
  public required string ServiceName { get; set; }

  /// <summary>
  /// Gets or sets the pod template for this StatefulSet.
  /// </summary>
  public required NativePodTemplateSpec Template { get; set; }

  /// <summary>
  /// Gets or sets the update strategy for this StatefulSet.
  /// </summary>
  public NativeStatefulSetUpdateStrategy? UpdateStrategy { get; set; }

  /// <summary>
  /// Gets or sets the minimum number of seconds for which a newly created StatefulSet pod should be ready without any of its container crashing.
  /// </summary>
  public int? MinReadySeconds { get; set; }

  /// <summary>
  /// Gets or sets the number of old history to retain to allow rollback.
  /// </summary>
  public int? RevisionHistoryLimit { get; set; }

  /// <summary>
  /// Gets or sets the pod management policy for this StatefulSet.
  /// </summary>
  public NativeStatefulSetPodManagementPolicy? PodManagementPolicy { get; set; }

  /// <summary>
  /// Gets or sets the persistentVolumeClaimRetentionPolicy for this StatefulSet.
  /// </summary>
  public NativeStatefulSetPersistentVolumeClaimRetentionPolicy? PersistentVolumeClaimRetentionPolicy { get; set; }
}
