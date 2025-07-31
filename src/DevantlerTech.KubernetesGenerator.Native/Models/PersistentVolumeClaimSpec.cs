namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a PersistentVolumeClaim.
/// </summary>
public class PersistentVolumeClaimSpec
{
  /// <summary>
  /// Gets or sets the access modes for the PersistentVolumeClaim.
  /// </summary>
  public required IList<PersistentVolumeAccessMode> AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the data source for the PersistentVolumeClaim.
  /// </summary>
  public TypedLocalObjectReference? DataSource { get; set; }

  /// <summary>
  /// Gets or sets the data source reference for the PersistentVolumeClaim.
  /// </summary>
  public TypedObjectReference? DataSourceRef { get; set; }

  /// <summary>
  /// Gets or sets the resource requirements for the PersistentVolumeClaim.
  /// </summary>
  public VolumeResourceRequirements? Resources { get; set; }

  /// <summary>
  /// Gets or sets the label selector for the PersistentVolumeClaim.
  /// </summary>
  public LabelSelector? Selector { get; set; }

  /// <summary>
  /// Gets or sets the storage class name for the PersistentVolumeClaim.
  /// </summary>
  public string? StorageClassName { get; set; }

  /// <summary>
  /// Gets or sets the volume mode for the PersistentVolumeClaim.
  /// </summary>
  public VolumeMode? VolumeMode { get; set; }

  /// <summary>
  /// Gets or sets the volume name for the PersistentVolumeClaim.
  /// </summary>
  public string? VolumeName { get; set; }
}
