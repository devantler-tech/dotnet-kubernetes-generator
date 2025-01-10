namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A reference to a Secret.
/// </summary>
public class FluxSecretRef
{
  /// <summary>
  /// The name of the Secret.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The key in the Secret.
  /// </summary>
  public string? Key { get; set; }
}
