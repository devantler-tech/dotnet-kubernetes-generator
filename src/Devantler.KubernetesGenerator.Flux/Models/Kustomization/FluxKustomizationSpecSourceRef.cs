namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// Flux kustomization source reference.
/// </summary>
public class FluxKustomizationSpecSourceRef : IFluxSourceRef<FluxKustomizationSpecSourceRefKind>
{
  /// <summary>
  /// The kind of the source reference.
  /// </summary>
  public required FluxKustomizationSpecSourceRefKind Kind { get; set; } = FluxKustomizationSpecSourceRefKind.OCIRepository;

  /// <summary>
  /// The name of the source reference.
  /// </summary>
  public required string Name { get; set; } = "flux-system";

  /// <summary>
  /// The namespace of the source reference.
  /// </summary>
  public string? Namespace { get; set; }
}
