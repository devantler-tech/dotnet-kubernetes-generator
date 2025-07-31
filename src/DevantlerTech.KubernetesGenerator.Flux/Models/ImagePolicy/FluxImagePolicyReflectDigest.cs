namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// The digest reflection policy for an ImagePolicy.
/// </summary>
public enum FluxImagePolicyReflectDigest
{
  /// <summary>
  /// Never reflect digest.
  /// </summary>
  Never,

  /// <summary>
  /// Reflect digest if not present.
  /// </summary>
  IfNotPresent,

  /// <summary>
  /// Always reflect digest.
  /// </summary>
  Always
}