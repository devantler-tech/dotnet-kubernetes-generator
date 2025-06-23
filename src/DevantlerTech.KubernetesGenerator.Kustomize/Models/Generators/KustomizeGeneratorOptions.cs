namespace DevantlerTech.KubernetesGenerator.Kustomize.Models.Generators;

/// <summary>
/// Options for the kustomize generator.
/// </summary>
public class KustomizeGeneratorOptions
{
  /// <summary>
  /// Disable the name suffix hash. (e.g. `my-configmap-1234567890` -> `my-configmap`)
  /// </summary>
  public bool? DisableNameSuffixHash { get; set; }

  /// <summary>
  /// Labels to add to the generated ConfigMap.
  /// </summary>
  public IEnumerable<object>? Labels { get; set; }

  /// <summary>
  /// Annotations to add to the generated ConfigMap.
  /// </summary>
  public IEnumerable<object>? Annotations { get; set; }
}
