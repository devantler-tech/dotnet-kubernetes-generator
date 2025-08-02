namespace DevantlerTech.KubernetesGenerator.Native.Models.Secret;

/// <summary>
/// Base class for Kubernetes secret models.
/// </summary>
public abstract class NativeSecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }
}
