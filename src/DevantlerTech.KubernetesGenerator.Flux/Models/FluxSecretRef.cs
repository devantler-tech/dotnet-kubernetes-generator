namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// A reference to a Secret.
/// </summary>
public class FluxSecretRef
{
  /// <summary>
  /// The name of the Secret.
  /// </summary>
  public required string Name { get; set; }
}
