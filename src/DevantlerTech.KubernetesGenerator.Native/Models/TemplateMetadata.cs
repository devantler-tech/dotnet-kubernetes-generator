using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents metadata for templates (labels and annotations only, no name or namespace).
/// This is used for resource templates where only labels and annotations are needed.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a template metadata class intended for reuse in templates")]
public class TemplateMetadata
{
  /// <summary>
  /// Gets or sets the labels for this object.
  /// </summary>
  public IDictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Gets or sets the annotations for this object.
  /// </summary>
  public IDictionary<string, string>? Annotations { get; init; }
}
