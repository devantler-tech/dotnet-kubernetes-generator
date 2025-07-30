namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Base class for Kubernetes secret models.
/// </summary>
public abstract class BaseSecret(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };
}