namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Install and upgrade options for a HelmRelease.
/// </summary>
public class FluxHelmReleaseSpecInstallUpgrade
{
  /// <summary>
  /// Options for CRDs when installing or upgrading a HelmRelease.
  /// </summary>
  public required FluxHelmReleaseSpecInstallUpgradeCrds Crds { get; set; }
}
