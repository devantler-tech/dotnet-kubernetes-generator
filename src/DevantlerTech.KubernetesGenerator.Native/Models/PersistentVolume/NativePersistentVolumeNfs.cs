namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents an NFS configuration for a PersistentVolume.
/// </summary>
public class NativePersistentVolumeNfs
{
  /// <summary>
  /// Gets or sets the NFS server address.
  /// </summary>
  public required string Server { get; set; }

  /// <summary>
  /// Gets or sets the path on the NFS server.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets whether the NFS mount is read-only.
  /// </summary>
  public bool? ReadOnly { get; set; }
}
