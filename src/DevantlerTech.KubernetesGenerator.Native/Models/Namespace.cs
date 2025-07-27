namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Namespace for use with kubectl create namespace.
/// </summary>
public class Namespace
{
  /// <summary>
  /// Gets or sets the API version of this Namespace.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "Namespace";

  /// <summary>
  /// Gets or sets the metadata for the namespace.
  /// </summary>
  public required ClusterScopedMetadata Metadata { get; set; }
}