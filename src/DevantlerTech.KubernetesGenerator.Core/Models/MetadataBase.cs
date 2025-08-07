using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Core.Models;

/// <summary>
/// Base class for metadata with common properties for all Kubernetes resources.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a base metadata class intended for reuse across different implementations")]
public abstract class MetadataBase
{
  /// <summary>
  /// Gets or sets the name of the resource.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the labels for this object.
  /// </summary>
  public IDictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Gets or sets the annotations for this object.
  /// </summary>
  public IDictionary<string, string>? Annotations { get; init; }
}