using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents metadata for a role binding.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a generic metadata class intended for reuse across different implementations")]
public class Metadata
{
  /// <summary>
  /// Gets or sets the name of the role binding.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the role binding.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// Gets or sets the labels for this object.
  /// </summary>
  public IDictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Gets or sets the annotations for this object.
  /// </summary>
  public IDictionary<string, string>? Annotations { get; init; }
}
