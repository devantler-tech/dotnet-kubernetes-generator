namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// Object to reference a ConfigMap or Secret.
/// </summary>
public class FluxConfigRef
{
  /// <summary>
  /// The name of the ConfigMap or Secret.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The kind of the ConfigMap or Secret.
  /// </summary>
  public required FluxConfigRefKind Kind { get; set; }
}
