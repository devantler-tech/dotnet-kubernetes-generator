namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A Flux HelmRelease.
/// </summary>
public class FluxHelmRelease
{
  /// <summary>
  /// Metadata of the HelmRelease.
  /// </summary>
  public required FluxHelmReleaseMetadata Metadata { get; set; }

  /// <summary>
  /// Spec of the HelmRelease.
  /// </summary>
  public required FluxHelmReleaseSpec Spec { get; set; }
}
