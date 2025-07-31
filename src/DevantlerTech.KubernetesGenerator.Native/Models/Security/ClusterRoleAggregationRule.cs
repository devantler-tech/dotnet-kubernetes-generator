namespace DevantlerTech.KubernetesGenerator.Native.Models.Security;

/// <summary>
/// Represents an aggregation rule for combining cluster roles.
/// </summary>
public class ClusterRoleAggregationRule
{
  /// <summary>
  /// Gets or sets the cluster role selectors for aggregation.
  /// </summary>
  public required IList<ClusterRoleLabelSelector> ClusterRoleSelectors { get; init; } = [];
}
