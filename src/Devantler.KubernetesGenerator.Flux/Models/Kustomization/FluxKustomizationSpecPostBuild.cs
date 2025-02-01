namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// Post build varialbes for the Flux Kustomization object.
/// </summary>
public class FluxKustomizationSpecPostBuild
{
  /// <summary>
  /// Key-value pairs to substitute in the resources after kustomize build.
  /// </summary>
  public IDictionary<string, string>? Substitute { get; set; }

  /// <summary>
  /// A list of sources to substitute from.
  /// </summary>
  public IEnumerable<FluxKustomizationSpecPostBuildSubstituteFrom>? SubstituteFrom { get; set; }
}
