namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// A Flux ImagePolicy.
/// </summary>
public class FluxImagePolicy
{
  /// <summary>
  /// Metadata of the ImagePolicy.
  /// </summary>
  public required FluxNamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Spec of the ImagePolicy.
  /// </summary>
  public required FluxImagePolicySpec Spec { get; set; }
}