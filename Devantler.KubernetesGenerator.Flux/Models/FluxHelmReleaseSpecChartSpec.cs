using Devantler.KubernetesGenerator.Flux.Models.Sources;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Spec of the chart to install.
/// </summary>
public class FluxHelmReleaseSpecChartSpec
{
  /// <summary>
  /// The name of the chart to install.
  /// </summary>
  public required string Chart { get; set; }

  /// <summary>
  /// The version of the chart to install.
  /// </summary>
  public required string Version { get; set; }

  /// <summary>
  /// The source reference of the chart to install.
  /// </summary>
  public required FluxSourceRef SourceRef { get; set; }
}
