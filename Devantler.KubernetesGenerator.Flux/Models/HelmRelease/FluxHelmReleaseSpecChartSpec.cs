namespace Devantler.KubernetesGenerator.Flux.Models.HelmRelease;

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
  /// The interval of which to check for new chart versions.
  /// </summary>
  public string? Interval { get; set; }

  /// <summary>
  /// The version of the chart to install.
  /// </summary>
  public string? Version { get; set; }

  /// <summary>
  /// The reconcile strategy to use.
  /// </summary>
  public FluxHelmReleaseSpecChartSpecReconcileStrategy? ReconcileStrategy { get; set; }

  /// <summary>
  /// The source reference of the chart to install.
  /// </summary>
  public FluxSourceRef? SourceRef { get; set; }
}
