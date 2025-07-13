namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes ClusterRole for use with kubectl create clusterrole.
/// </summary>
public class ClusterRole
{
  /// <summary>
  /// Gets or sets the name of the ClusterRole.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the rules for the ClusterRole.
  /// </summary>
  public IEnumerable<PolicyRule>? Rules { get; set; }

  /// <summary>
  /// Gets or sets the aggregation rule for the ClusterRole.
  /// </summary>
  public AggregationRule? AggregationRule { get; set; }
}