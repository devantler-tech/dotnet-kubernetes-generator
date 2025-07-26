namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an NFS configuration for a PersistentVolume.
/// </summary>
public class PersistentVolumeNfs
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