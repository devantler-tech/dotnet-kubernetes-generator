namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Spec of a Flux HelmRelease.
/// </summary>
public class FluxHelmReleaseSpec
{
  /// <summary>
  /// Interval to check for new releases. Go duration string format e.g. 5m, 1h.
  /// </summary>
  public string? Interval { get; set; }

  /// <summary>
  /// Chart to install.
  /// </summary>
  public FluxHelmReleaseSpecChart? Chart { get; set; }

  /// <summary>
  /// Chart reference if the HelmRelease should be installed from a chart.
  /// </summary>
  public FluxHelmReleaseChartRef? ChartRef { get; set; }
}
