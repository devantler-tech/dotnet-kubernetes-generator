namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// ConfigMap or Secret to indicate the kind of resource to substitute post build variables from.
/// </summary>
public enum FluxKustomizationSpecPostBuildSubstituteFromKind
{
  /// <summary>
  /// A ConfigMap.
  /// </summary>
  ConfigMap,
  /// <summary>
  /// A Secret.
  /// </summary>
  Secret
}
