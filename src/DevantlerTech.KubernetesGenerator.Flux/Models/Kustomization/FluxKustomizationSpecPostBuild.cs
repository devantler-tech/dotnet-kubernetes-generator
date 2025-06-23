namespace DevantlerTech.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// Post build varialbes for the Flux Kustomization object.
/// </summary>
public class FluxKustomizationSpecPostBuild(IDictionary<string, string>? substitute = default)
{
  /// <summary>
  /// Key-value pairs to substitute in the resources after kustomize build.
  /// </summary>
  public IDictionary<string, string>? Substitute { get; } = substitute;

  /// <summary>
  /// A list of sources to substitute from.
  /// </summary>
  public IEnumerable<FluxKustomizationSpecPostBuildSubstituteFrom>? SubstituteFrom { get; set; }
}
