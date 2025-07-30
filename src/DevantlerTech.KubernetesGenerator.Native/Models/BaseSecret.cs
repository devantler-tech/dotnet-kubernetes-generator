namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Base class for Kubernetes secret models.
/// </summary>
public abstract class BaseSecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public required Metadata Metadata { get; set; }
}
