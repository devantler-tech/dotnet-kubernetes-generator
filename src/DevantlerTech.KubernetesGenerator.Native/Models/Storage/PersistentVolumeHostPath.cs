namespace DevantlerTech.KubernetesGenerator.Native.Models.Storage;

/// <summary>
/// Represents a host path configuration for a PersistentVolume.
/// </summary>
public class PersistentVolumeHostPath
{
  /// <summary>
  /// Gets or sets the path on the host.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets the type of the host path.
  /// </summary>
  public PersistentVolumeHostPathType? Type { get; set; }
}
