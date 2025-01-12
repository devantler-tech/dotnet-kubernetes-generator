namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// The kind of the chart reference.
/// </summary>
public enum FluxHelmReleaseSpecChartRefKind
{
  /// <summary>
  /// The chart reference is a HelmChart.
  /// </summary>
  HelmChart,

  /// <summary>
  /// The chart reference is a OCIRepository.
  /// </summary>
  OCIRepository
}
