namespace Devantler.KubernetesGenerator.Flux.Models.Sources;

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
  /// The source reference is a OCIRepository.
  /// </summary>
  OCIRepository,

  /// <summary>
  /// The source reference is a Bucket.
  /// </summary>
  Bucket
}
