namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Post build operation to perform after the Kustomization has been applied.
/// </summary>
public class FluxKustomizationSpecPostBuild
{
  /// <summary>
  /// A map of key-value pairs to substitute post build variables with in the Kustomization's resources.
  /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
  public IDictionary<string, string>? Substitute { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

  /// <summary>
  /// A list of ConfigMaps or Secrets to substitute post build variables with in the Kustomization's resources.
  /// </summary>
  public IEnumerable<FluxKustomizationSpecPostBuildSubstituteFrom>? SubstituteFrom { get; set; }
}
