#pragma warning disable CA2227
namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Post build operation to perform after the Kustomization has been applied.
/// </summary>
public class FluxKustomizationSpecPostBuild
{
  /// <summary>
  /// A map of key-value pairs to substitute post build variables with in the Kustomization's resources.
  /// </summary>
  public IDictionary<string, string>? Substitute { get; set; }

  /// <summary>
  /// A list of ConfigMaps or Secrets to substitute post build variables with in the Kustomization's resources.
  /// </summary>
  public IEnumerable<FluxKustomizationSpecPostBuildSubstituteFrom>? SubstituteFrom { get; set; }
}
