namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The type of the source reference.
/// </summary>
public enum FluxSource
{
  /// <summary>
  /// The source reference is a GitRepository.
  /// </summary>
  GitRepository,

  /// <summary>
  /// The source reference is a HelmRepository.
  /// </summary>
  HelmRepository,

  /// <summary>
  /// The source reference is a Bucket.
  /// </summary>
  Bucket
}
