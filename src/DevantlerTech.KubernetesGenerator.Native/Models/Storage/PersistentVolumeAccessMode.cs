namespace DevantlerTech.KubernetesGenerator.Native.Models.Storage;

/// <summary>
/// Enum for supported PersistentVolume access modes.
/// </summary>
public enum PersistentVolumeAccessMode
{
  /// <summary>
  /// The volume can be mounted as read-write by a single node.
  /// </summary>
  ReadWriteOnce,

  /// <summary>
  /// The volume can be mounted as read-only by many nodes.
  /// </summary>
  ReadOnlyMany,

  /// <summary>
  /// The volume can be mounted as read-write by many nodes.
  /// </summary>
  ReadWriteMany,

  /// <summary>
  /// The volume can be mounted as read-write by a single Pod.
  /// </summary>
  ReadWriteOncePod
}
