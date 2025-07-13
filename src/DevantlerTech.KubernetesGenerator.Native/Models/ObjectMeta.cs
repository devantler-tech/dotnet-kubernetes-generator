namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents basic metadata for Kubernetes objects.
/// </summary>
public class ObjectMeta
{
  /// <summary>
  /// Gets or sets the name of the object. This is required for all Kubernetes objects.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the object. Optional for cluster-scoped resources.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// Gets or sets the labels for the object. Optional.
  /// </summary>
  public Dictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Gets or sets the annotations for the object. Optional.
  /// </summary>
  public Dictionary<string, string>? Annotations { get; init; }
}