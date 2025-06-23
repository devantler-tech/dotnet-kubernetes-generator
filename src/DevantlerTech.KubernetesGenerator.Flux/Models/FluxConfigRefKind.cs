namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// Kind
/// </summary>
public enum FluxConfigRefKind
{
  /// <summary>
  /// A kubernetes ConfigMap.
  /// </summary>
  ConfigMap,

  /// <summary>
  /// A kubernetes Secret.
  /// </summary>
  Secret
}
