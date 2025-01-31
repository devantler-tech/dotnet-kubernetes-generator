namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

/// <summary>
/// Source reference of the chart to install.
/// </summary>
public class FluxHelmReleaseSpecChartSpecSourceRef : IFluxSourceRef<FluxHelmReleaseSpecChartSpecSourceRefKind>

{
  /// <summary>
  /// The kind of the source reference.
  /// </summary>
  public required FluxHelmReleaseSpecChartSpecSourceRefKind Kind { get; set; }

  /// <summary>
  /// The name of the source reference.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the source reference.
  /// </summary>
  public string? Namespace { get; set; }
}
