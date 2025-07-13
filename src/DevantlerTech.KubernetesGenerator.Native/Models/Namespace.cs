using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes namespace for use with kubectl create namespace.
/// </summary>
public class Namespace
{
  /// <summary>
  /// Gets or sets the metadata for the namespace.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the name of the namespace.
  /// </summary>
  public required string Name { get; set; }
}