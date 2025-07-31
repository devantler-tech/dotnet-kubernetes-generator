using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents metadata for namespace-scoped resources.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a generic metadata class intended for reuse across different implementations")]
public class NativeMetadata : BaseMetadata
{
  /// <summary>
  /// Gets or sets the namespace of the resource.
  /// </summary>
  public string? Namespace { get; set; }
}
