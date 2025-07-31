namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolumeClaim;

/// <summary>
/// Represents a Kubernetes PersistentVolumeClaim.
/// </summary>
public class NativePersistentVolumeClaim
{
  /// <summary>
  /// Gets or sets the API version of this PersistentVolumeClaim.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "PersistentVolumeClaim";

  /// <summary>
  /// Gets or sets the metadata for the PersistentVolumeClaim.
  /// </summary>
  public required NativeMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the PersistentVolumeClaim.
  /// </summary>
  public required NativePersistentVolumeClaimSpec Spec { get; init; }
}
