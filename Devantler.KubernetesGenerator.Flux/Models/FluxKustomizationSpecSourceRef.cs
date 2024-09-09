using Devantler.KubernetesGenerator.Flux.Models.Sources;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The source reference that the Kustomization object should be reconciled with.
/// </summary>
public class FluxKustomizationSpecSourceRef
{
  /// <summary>
  /// The kind of the source reference.
  /// </summary>
  public required FluxSource Kind { get; set; }

  /// <summary>
  /// The name of the source reference.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the source reference.
  /// </summary>
  public string? Namespace { get; set; }
}
