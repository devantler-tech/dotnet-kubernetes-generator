namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a ConfigMap for use with kubectl create configmap.
/// </summary>
public class ConfigMap
{
  /// <summary>
  /// Gets or sets the name of the ConfigMap.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the data entries for the ConfigMap.
  /// </summary>
  public Dictionary<string, string>? Data { get; init; }

  /// <summary>
  /// Gets or sets the namespace for the ConfigMap.
  /// </summary>
  public string? Namespace { get; set; }
}