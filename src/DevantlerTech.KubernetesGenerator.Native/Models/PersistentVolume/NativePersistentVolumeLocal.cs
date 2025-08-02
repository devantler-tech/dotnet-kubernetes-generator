namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents a local storage configuration for a PersistentVolume.
/// </summary>
public class NativePersistentVolumeLocal
{
  /// <summary>
  /// Gets or sets the path for the local storage.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets the filesystem type.
  /// </summary>
  public string? FsType { get; set; }
}
