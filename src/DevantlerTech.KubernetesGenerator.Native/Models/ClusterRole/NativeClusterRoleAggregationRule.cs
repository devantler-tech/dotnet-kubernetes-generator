namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRole;

/// <summary>
/// Represents an aggregation rule for combining cluster roles.
/// </summary>
public class NativeClusterRoleAggregationRule
{
  /// <summary>
  /// Gets or sets the cluster role selectors for aggregation.
  /// </summary>
  public required IList<NativeClusterRoleLabelSelector> ClusterRoleSelectors { get; init; } = [];
}
