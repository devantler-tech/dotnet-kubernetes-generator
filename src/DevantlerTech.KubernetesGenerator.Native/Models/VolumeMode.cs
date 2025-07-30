namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Enum for supported volume modes.
/// </summary>
public enum VolumeMode
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
