namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Reference to a HelmRelease chart.
/// </summary>
public class FluxHelmReleaseSpecChartRef
{
  /// <summary>
  /// The kind of the chart reference.
  /// </summary>
  public required FluxHelmReleaseSpecChartRefKind Kind { get; set; }

  /// <summary>
  /// The name of the chart reference.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the chart reference.
  /// </summary>
  public string? Namespace { get; set; }
}
