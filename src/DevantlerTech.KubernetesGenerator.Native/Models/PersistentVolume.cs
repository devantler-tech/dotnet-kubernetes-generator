namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes PersistentVolume for use with kubectl create.
/// </summary>
public class PersistentVolume
{
  /// <summary>
  /// Gets or sets the API version of this PersistentVolume.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "PersistentVolume";

  /// <summary>
  /// Gets or sets the metadata for the persistent volume.
  /// </summary>
  public required ClusterScopedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the persistent volume.
  /// </summary>
  public required PersistentVolumeSpec Spec { get; init; }
}