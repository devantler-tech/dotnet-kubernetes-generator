namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Options for CRDs when installing or upgrading a HelmRelease.
/// </summary>
public enum FluxHelmReleaseSpecInstallUpgradeCrds
{
  /// <summary>
  /// Skip CRDs.
  /// </summary>
  Skip,

  /// <summary>
  /// Create CRDs.
  /// </summary>
  Create,

  /// <summary>
  /// Create and replace CRDs.
  /// </summary>
  CreateReplace
}
