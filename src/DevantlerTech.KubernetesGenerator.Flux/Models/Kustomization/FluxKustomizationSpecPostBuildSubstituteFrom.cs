namespace DevantlerTech.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// A source to substitute from in the Flux Kustomization object.
/// </summary>
public class FluxKustomizationSpecPostBuildSubstituteFrom
{
  /// <summary>
  /// The kind of the source to substitute from.
  /// </summary>
  public required FluxConfigRefKind Kind { get; set; }

  /// <summary>
  /// The name of the source to substitute from.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Whether the source is optional.
  /// </summary>
  public bool Optional { get; set; }
}
