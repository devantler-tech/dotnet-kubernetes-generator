namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolumeClaim;

/// <summary>
/// Represents the specification for a PersistentVolumeClaim.
/// </summary>
public class NativePersistentVolumeClaimSpec
{
  /// <summary>
  /// Gets or sets the access modes for the PersistentVolumeClaim.
  /// </summary>
  public required IList<NativePersistentVolumeAccessMode> AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the data source for the PersistentVolumeClaim.
  /// </summary>
  public NativeTypedLocalObjectReference? DataSource { get; set; }

  /// <summary>
  /// Gets or sets the data source reference for the PersistentVolumeClaim.
  /// </summary>
  public NativeTypedObjectReference? DataSourceRef { get; set; }

  /// <summary>
  /// Gets or sets the resource requirements for the PersistentVolumeClaim.
  /// </summary>
  public NativeVolumeResourceRequirements? Resources { get; set; }

  /// <summary>
  /// Gets or sets the label selector for the PersistentVolumeClaim.
  /// </summary>
  public NativeLabelSelector? Selector { get; set; }

  /// <summary>
  /// Gets or sets the storage class name for the PersistentVolumeClaim.
  /// </summary>
  public string? StorageClassName { get; set; }

  /// <summary>
  /// Gets or sets the volume mode for the PersistentVolumeClaim.
  /// </summary>
  public NativeVolumeMode? VolumeMode { get; set; }

  /// <summary>
  /// Gets or sets the volume name for the PersistentVolumeClaim.
  /// </summary>
  public string? VolumeName { get; set; }
}
