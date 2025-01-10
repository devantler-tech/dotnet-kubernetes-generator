namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Reference to a HelmRelease chart.
/// </summary>
public class FluxHelmReleaseChartRef
{
  /// <summary>
  /// The kind of the chart reference.
  /// </summary>
  public required FluxHelmReleaseChartRefKind Kind { get; set; }

  /// <summary>
  /// The name of the chart reference.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the chart reference.
  /// </summary>
  public string Namespace { get; set; }
}
