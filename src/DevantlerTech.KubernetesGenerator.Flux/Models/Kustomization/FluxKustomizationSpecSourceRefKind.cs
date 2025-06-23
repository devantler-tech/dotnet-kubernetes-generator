namespace DevantlerTech.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// The kind of flux sources.
/// </summary>
public enum FluxKustomizationSpecSourceRefKind
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
  /// The source is a Bucket.
  /// </summary>
  Bucket
}
