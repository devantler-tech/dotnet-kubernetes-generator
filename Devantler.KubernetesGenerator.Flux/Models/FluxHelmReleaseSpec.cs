#pragma warning disable CA2227 // Collection properties should be read only
using Devantler.KubernetesGenerator.Flux.Models.Dependencies;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Spec of a Flux HelmRelease.
/// </summary>
public class FluxHelmReleaseSpec
{
  /// <summary>
  /// Interval to check for new releases. Go duration string format e.g. 5m, 1h.
  /// </summary>
  public required string Interval { get; set; }

  /// <summary>
  /// Dependencies of the HelmRelease.
  /// </summary>
  public IEnumerable<FluxDependsOn>? DependsOn { get; set; }

  /// <summary>
  /// Chart to install.
  /// </summary>
  public required FluxHelmReleaseSpecChart Chart { get; set; }

  /// <summary>
  /// Values to pass to the chart.
  /// </summary>
  public IDictionary<string, object>? Values { get; set; }
}
