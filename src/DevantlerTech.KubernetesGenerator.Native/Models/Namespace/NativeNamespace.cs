namespace DevantlerTech.KubernetesGenerator.Native.Models.Namespace;

/// <summary>
/// Represents a Kubernetes Namespace for use with kubectl create namespace.
/// </summary>
public class NativeNamespace
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
  public required NativeClusterScopedMetadata Metadata { get; set; }
}
