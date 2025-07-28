namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes ConfigMap.
/// </summary>
public class ConfigMap
{
  /// <summary>
  /// Gets or sets the API version of this ConfigMap.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "ConfigMap";

  /// <summary>
  /// Gets or sets the metadata for the ConfigMap.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the key-value pairs to include in the ConfigMap data.
  /// </summary>
  public required IDictionary<string, string> Data { get; init; }

  /// <summary>
  /// Gets or sets whether to append a hash of the ConfigMap to its name.
  /// </summary>
  public bool AppendHash { get; set; }
}