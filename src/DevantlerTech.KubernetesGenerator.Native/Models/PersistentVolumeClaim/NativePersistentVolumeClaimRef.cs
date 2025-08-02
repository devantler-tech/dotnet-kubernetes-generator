namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolumeClaim;

/// <summary>
/// Represents a persistent volume claim reference.
/// </summary>
public class NativePersistentVolumeClaimRef
{
  /// <summary>
  /// Gets or sets the API version.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind.
  /// </summary>
  public string Kind { get; set; } = "PersistentVolumeClaim";

  /// <summary>
  /// Gets or sets the name of the claim.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the claim.
  /// </summary>
  public string? Namespace { get; set; }
}
