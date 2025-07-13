using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes ConfigMap for use with kubectl create configmap.
/// </summary>
public class ConfigMap
{
  /// <summary>
  /// Gets or sets the metadata for the configmap.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Gets or sets the data for the configmap as key-value pairs.
  /// </summary>
  public Dictionary<string, string>? Data { get; init; }

  /// <summary>
  /// Gets or sets the files to load data from.
  /// </summary>
  public Collection<string>? FromFiles { get; init; }

  /// <summary>
  /// Gets or sets the environment files to load data from.
  /// </summary>
  public Collection<string>? FromEnvFiles { get; init; }

  /// <summary>
  /// Gets or sets the literal key-value pairs to add.
  /// </summary>
  public Dictionary<string, string>? FromLiterals { get; init; }
}