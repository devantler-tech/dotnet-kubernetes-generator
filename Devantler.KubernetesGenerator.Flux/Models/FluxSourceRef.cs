namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Flux source reference.
/// </summary>
public class FluxSourceRef
{
  /// <summary>
  /// The type of the source reference.
  /// </summary>
  public required FluxSourceRefKind Kind { get; set; }

  /// <summary>
  /// The name of the source reference.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the source reference.
  /// </summary>
  public string? Namespace { get; set; }
}
