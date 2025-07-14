namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Namespace for use with kubectl create namespace commands.
/// </summary>
public class Namespace(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the namespace.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets whether to save the configuration in the annotation.
  /// </summary>
  public bool SaveConfig { get; init; }

  /// <summary>
  /// Gets or sets the field manager name for tracking field ownership.
  /// </summary>
  public string? FieldManager { get; init; }
}