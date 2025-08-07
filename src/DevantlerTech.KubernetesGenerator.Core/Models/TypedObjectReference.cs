namespace DevantlerTech.KubernetesGenerator.Core.Models;

/// <summary>
/// Represents a typed object reference.
/// </summary>
public class TypedObjectReference
{
  /// <summary>
  /// Gets or sets the API group of the referent.
  /// </summary>
  public string? ApiGroup { get; set; }

  /// <summary>
  /// Gets or sets the kind of the referent.
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the referent.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the referent.
  /// </summary>
  public string? Namespace { get; set; }
}