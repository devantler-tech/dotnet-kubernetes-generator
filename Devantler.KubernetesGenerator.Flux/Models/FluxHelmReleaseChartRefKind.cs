namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The kind of the chart reference.
/// </summary>
public enum FluxHelmReleaseChartRefKind
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
