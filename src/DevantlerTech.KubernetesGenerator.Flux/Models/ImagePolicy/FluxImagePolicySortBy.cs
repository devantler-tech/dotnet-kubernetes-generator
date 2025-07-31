namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// The sorting strategy for an ImagePolicy.
/// </summary>
public enum FluxImagePolicySortBy
{
  /// <summary>
  /// Use semantic versioning to select image.
  /// </summary>
  Semver,

  /// <summary>
  /// Use numeric sorting to select image.
  /// </summary>
  Numeric,

  /// <summary>
  /// Use alphabetical sorting to select image.
  /// </summary>
  Alpha
}