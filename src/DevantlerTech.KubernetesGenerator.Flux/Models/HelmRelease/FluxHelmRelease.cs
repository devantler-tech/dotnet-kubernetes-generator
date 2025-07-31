using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// A Flux HelmRelease.
/// </summary>
public class FluxHelmRelease
{
  /// <summary>
  /// Metadata of the HelmRelease.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Spec of the HelmRelease.
  /// </summary>
  public required FluxHelmReleaseSpec Spec { get; set; }
}
