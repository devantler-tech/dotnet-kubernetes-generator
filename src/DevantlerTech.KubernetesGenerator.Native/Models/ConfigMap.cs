namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes ConfigMap for use with kubectl create configmap.
/// </summary>
public class ConfigMap(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the configmap.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets the configmap data as key-value pairs.
  /// </summary>
  public Dictionary<string, string> Data { get; } = [];
}