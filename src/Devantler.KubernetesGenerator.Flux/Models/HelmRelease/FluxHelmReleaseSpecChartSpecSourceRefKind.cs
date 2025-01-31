namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// The kind of the source reference.
/// </summary>
public enum FluxHelmReleaseSpecChartSpecSourceRefKind
{
  /// <summary>
  /// The source reference is a Helm repository.
  /// </summary>
  HelmRepository,

  /// <summary>
  /// The source reference is a Git repository.
  /// </summary>
  GitRepository,

  /// <summary>
  /// The source reference is an OCI repository.
  /// </summary>
  Bucket
}
