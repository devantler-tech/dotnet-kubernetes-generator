namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a file source for a ConfigMap.
/// </summary>
public class ConfigMapFile
{
  /// <summary>
  /// Gets or sets the key to use for this file in the ConfigMap.
  /// If null, the file basename will be used.
  /// </summary>
  public string? Key { get; set; }

  /// <summary>
  /// Gets or sets the path to the file.
  /// </summary>
  public required string FilePath { get; set; }
}