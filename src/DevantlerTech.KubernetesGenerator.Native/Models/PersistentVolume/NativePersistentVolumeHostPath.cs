namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents a host path configuration for a PersistentVolume.
/// </summary>
public class NativePersistentVolumeHostPath
{
  /// <summary>
  /// Gets or sets the path on the host.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets the type of the host path.
  /// </summary>
  public NativePersistentVolumeHostPathType? Type { get; set; }
}
