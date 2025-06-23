namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRelease;

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
  /// Install and upgrade options.
  /// </summary>
  public FluxHelmReleaseSpecInstallUpgrade? InstallUpgrade { get; set; }

  /// <summary>
  /// DependsOn is a list of HelmRelease names that must be present before this HelmRelease can be installed.
  /// </summary>
  public IEnumerable<FluxDependsOn>? DependsOn { get; set; }

  /// <summary>
  /// Options for setting remote kubeconfig.
  /// </summary>
  public FluxKubeconfig? Kubeconfig { get; set; }

  /// <summary>
  /// ServiceAccountName is the name of the ServiceAccount to use to install the Helm chart.
  /// </summary>
  public string? ServiceAccountName { get; set; }

  /// <summary>
  /// Chart to install.
  /// </summary>
  public FluxHelmReleaseSpecChart? Chart { get; set; }

  /// <summary>
  /// Chart reference if the HelmRelease should be installed from a chart.
  /// </summary>
  public FluxHelmReleaseSpecChartRef? ChartRef { get; set; }

  /// <summary>
  /// A reference to a ConfigMap or Secret.
  /// </summary>
  public FluxConfigRef? ValuesFrom { get; set; }

  /// <summary>
  /// Constructor for a HelmRelease spec with a chart.
  /// </summary>
  /// <param name="chart"></param>
  public FluxHelmReleaseSpec(FluxHelmReleaseSpecChart chart) => Chart = chart;

  /// <summary>
  /// Constructor for a HelmRelease spec with a chart reference.
  /// </summary>
  /// <param name="chartRef"></param>
  public FluxHelmReleaseSpec(FluxHelmReleaseSpecChartRef chartRef) => ChartRef = chartRef;
}
