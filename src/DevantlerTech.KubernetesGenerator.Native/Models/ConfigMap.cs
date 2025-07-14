using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a ConfigMap for use with kubectl create configmap.
/// </summary>
public class ConfigMap(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the ConfigMap.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets the ConfigMap data as key-value pairs from literals.
  /// </summary>
  public Dictionary<string, string> Data { get; } = [];

  /// <summary>
  /// Gets the list of files to include in the ConfigMap.
  /// </summary>
  public Collection<string> FromFiles { get; } = [];

  /// <summary>
  /// Gets the list of environment files to include in the ConfigMap.
  /// </summary>
  public Collection<string> FromEnvFiles { get; } = [];

  /// <summary>
  /// Gets or sets whether the ConfigMap should be immutable.
  /// </summary>
  public bool? Immutable { get; set; }
}