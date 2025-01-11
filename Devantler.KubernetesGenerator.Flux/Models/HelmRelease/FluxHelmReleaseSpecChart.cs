namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Chart to install.
/// </summary>
public class FluxHelmReleaseSpecChart
{
  /// <summary>
  /// Spec of the chart to install.
  /// </summary>
  public required FluxHelmReleaseSpecChartSpec Spec { get; set; }
}
