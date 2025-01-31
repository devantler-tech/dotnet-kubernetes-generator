namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Reconile strategies for HelmRelease.
/// </summary>
public enum FluxHelmReleaseSpecChartSpecReconcileStrategy
{
  /// <summary>
  /// Use the source revision for reconciliation.
  /// </summary>
  Revision,

  /// <summary>
  /// Use the chart revision for reconciliation.
  /// </summary>
  ChartRevision
}
