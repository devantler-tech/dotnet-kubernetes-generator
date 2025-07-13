namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents an aggregation rule for use with kubectl create clusterrole.
/// </summary>
public class AggregationRule
{
  /// <summary>
  /// Gets or sets the cluster role selectors for aggregation.
  /// </summary>
  public required IEnumerable<LabelSelector> ClusterRoleSelectors { get; set; }
}