using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Core.Models;

/// <summary>
/// Represents metadata for namespace-scoped resources.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a generic metadata class intended for reuse across different implementations")]
public class NamespacedMetadata : Metadata
{
  /// <summary>
  /// Initializes a new instance of the <see cref="NamespacedMetadata"/> class.
  /// </summary>
  public NamespacedMetadata()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="NamespacedMetadata"/> class.
  /// </summary>
  /// <param name="labels">The labels to set on the metadata.</param>
  public NamespacedMetadata(IDictionary<string, string>? labels) => Labels = labels;

  /// <summary>
  /// Gets or sets the namespace of the resource.
  /// </summary>
  public string? Namespace { get; set; }
}
