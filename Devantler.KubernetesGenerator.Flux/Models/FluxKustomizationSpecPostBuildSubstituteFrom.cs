namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A reference to a ConfigMap or Secret to substitute post build variables with in the Kustomization's resources.
/// </summary>
public class FluxKustomizationSpecPostBuildSubstituteFrom
{
  /// <summary>
  /// ConfigMap or Secret to indicate the kind of resource to substitute post build variables from.
  /// </summary>
  public required FluxKustomizationSpecPostBuildSubstituteFromKind Kind { get; set; }

  /// <summary>
  /// The name of the ConfigMap or Secret to substitute post build variables from.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Whether to ignore the error if the ConfigMap or Secret does not exist.
  /// </summary>
  public bool? Optional { get; set; }
}
