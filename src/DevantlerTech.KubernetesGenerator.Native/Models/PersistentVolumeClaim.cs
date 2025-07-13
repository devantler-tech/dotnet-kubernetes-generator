using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a PersistentVolumeClaim for use with kubectl create -f.
/// </summary>
public class PersistentVolumeClaim
{
  /// <summary>
  /// Gets or sets the metadata for the persistent volume claim.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the access modes for the persistent volume claim.
  /// </summary>
  public required IList<string> AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the storage size for the persistent volume claim.
  /// </summary>
  public required string StorageSize { get; set; }

  /// <summary>
  /// Gets or sets the storage class name for the persistent volume claim.
  /// </summary>
  public string? StorageClassName { get; set; }

  /// <summary>
  /// Gets or sets the volume mode for the persistent volume claim.
  /// </summary>
  public string? VolumeMode { get; set; }

  /// <summary>
  /// Gets or sets the volume name for the persistent volume claim.
  /// </summary>
  public string? VolumeName { get; set; }

  /// <summary>
  /// Gets or sets the selector for the persistent volume claim.
  /// </summary>
  public V1LabelSelector? Selector { get; set; }

  /// <summary>
  /// Gets or sets the data source for the persistent volume claim.
  /// </summary>
  public V1TypedLocalObjectReference? DataSource { get; set; }

  /// <summary>
  /// Gets or sets the data source reference for the persistent volume claim.
  /// </summary>
  public V1TypedObjectReference? DataSourceRef { get; set; }
}