namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Enum for supported PersistentVolume HostPath types.
/// </summary>
public enum NativePersistentVolumeHostPathType
{
  /// <summary>
  /// Directory or create if it doesn't exist.
  /// </summary>
  DirectoryOrCreate,

  /// <summary>
  /// Directory must exist.
  /// </summary>
  Directory,

  /// <summary>
  /// File or create if it doesn't exist.
  /// </summary>
  FileOrCreate,

  /// <summary>
  /// File must exist.
  /// </summary>
  File,

  /// <summary>
  /// Socket must exist.
  /// </summary>
  Socket,

  /// <summary>
  /// Character device must exist.
  /// </summary>
  CharDevice,

  /// <summary>
  /// Block device must exist.
  /// </summary>
  BlockDevice
}
