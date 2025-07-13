using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes PersistentVolume for use with kubectl create -f.
/// </summary>
public class PersistentVolume
{
  /// <summary>
  /// Gets or sets the metadata for the persistent volume.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the capacity of the persistent volume.
  /// </summary>
  public required Dictionary<string, ResourceQuantity> Capacity { get; init; }

  /// <summary>
  /// Gets or sets the access modes for the persistent volume.
  /// </summary>
  public required IList<string> AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the host path volume source.
  /// </summary>
  public V1HostPathVolumeSource? HostPath { get; set; }

  /// <summary>
  /// Gets or sets the NFS volume source.
  /// </summary>
  public V1NFSVolumeSource? Nfs { get; set; }

  /// <summary>
  /// Gets or sets the storage class name.
  /// </summary>
  public string? StorageClassName { get; set; }

  /// <summary>
  /// Gets or sets the persistent volume reclaim policy.
  /// </summary>
  public string? PersistentVolumeReclaimPolicy { get; set; }

  /// <summary>
  /// Gets or sets the mount options.
  /// </summary>
  public IList<string>? MountOptions { get; init; }

  /// <summary>
  /// Gets or sets the node affinity for the persistent volume.
  /// </summary>
  public V1VolumeNodeAffinity? NodeAffinity { get; set; }

  /// <summary>
  /// Gets or sets the claim reference for the persistent volume.
  /// </summary>
  public V1ObjectReference? ClaimRef { get; set; }
}