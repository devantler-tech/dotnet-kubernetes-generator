namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolumeClaim;

/// <summary>
/// Enum for supported volume modes.
/// </summary>
public enum NativeVolumeMode
{
  /// <summary>
  /// Indicates that the volume will be used with a filesystem.
  /// </summary>
  Filesystem,

  /// <summary>
  /// Indicates that the volume will be used as a raw block device.
  /// </summary>
  Block
}
