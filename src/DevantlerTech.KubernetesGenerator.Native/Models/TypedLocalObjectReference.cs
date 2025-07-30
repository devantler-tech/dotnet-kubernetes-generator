namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a typed local object reference.
/// </summary>
public class TypedLocalObjectReference
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
}