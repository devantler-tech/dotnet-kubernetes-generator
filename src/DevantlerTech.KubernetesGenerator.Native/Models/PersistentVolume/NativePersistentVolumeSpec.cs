namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents the specification for a PersistentVolume.
/// </summary>
public class NativePersistentVolumeSpec
{
  /// <summary>
  /// Gets or sets the capacity of the persistent volume.
  /// </summary>
  public required IDictionary<string, string> Capacity { get; init; }

  /// <summary>
  /// Gets or sets the access modes for the persistent volume.
  /// </summary>
  public required IList<NativePersistentVolumeAccessMode> AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the reclaim policy for the persistent volume.
  /// </summary>
  public NativePersistentVolumeReclaimPolicy? PersistentVolumeReclaimPolicy { get; set; }

  /// <summary>
  /// Gets or sets the storage class name for the persistent volume.
  /// </summary>
  public string? StorageClassName { get; set; }

  /// <summary>
  /// Gets or sets the mount options for the persistent volume.
  /// </summary>
  public IList<string>? MountOptions { get; init; }

  /// <summary>
  /// Gets or sets the claim reference for the persistent volume.
  /// </summary>
  public NativePersistentVolumeClaimRef? ClaimRef { get; set; }

  /// <summary>
  /// Gets or sets the node affinity for the persistent volume.
  /// </summary>
  public NativePersistentVolumeNodeAffinity? NodeAffinity { get; set; }

  /// <summary>
  /// Gets or sets the host path for the persistent volume.
  /// </summary>
  public NativePersistentVolumeHostPath? HostPath { get; set; }

  /// <summary>
  /// Gets or sets the NFS configuration for the persistent volume.
  /// </summary>
  public NativePersistentVolumeNfs? Nfs { get; set; }

  /// <summary>
  /// Gets or sets the local configuration for the persistent volume.
  /// </summary>
  public NativePersistentVolumeLocal? Local { get; set; }
}
