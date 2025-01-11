namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The kind of flux sources.
/// </summary>
public enum FluxSourceRefKind
{
  /// <summary>
  /// The source is a GitRepository.
  /// </summary>
  GitRepository,

  /// <summary>
  /// The source is a OCIRepository.
  /// </summary>
  OCIRepository,

  /// <summary>
  /// The source is a HelmRepository.
  /// </summary>
  HelmRepository,

  /// <summary>
  /// The source is a Bucket.
  /// </summary>
  Bucket
}
