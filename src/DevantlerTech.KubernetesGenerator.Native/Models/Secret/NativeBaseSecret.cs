namespace DevantlerTech.KubernetesGenerator.Native.Models.Secret;

/// <summary>
/// Base class for Kubernetes secret models.
/// </summary>
public abstract class BaseSecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public required NativeMetadata Metadata { get; set; }
}
