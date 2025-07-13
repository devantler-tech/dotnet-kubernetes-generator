namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Namespace for use with kubectl create namespace.
/// </summary>
public class Namespace
{
  /// <summary>
  /// Gets or sets the metadata for the namespace.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }
}