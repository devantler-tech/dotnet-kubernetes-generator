namespace DevantlerTech.KubernetesGenerator.Native.Models;

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

/// <summary>
/// Represents a label selector for cluster role aggregation.
/// </summary>
public class ClusterRoleLabelSelector
{
  /// <summary>
  /// Gets or sets the match labels for the selector.
  /// </summary>
  public IDictionary<string, string>? MatchLabels { get; init; }

  /// <summary>
  /// Gets or sets the match expressions for the selector.
  /// </summary>
  public IList<ClusterRoleLabelSelectorRequirement>? MatchExpressions { get; init; }
}

/// <summary>
/// Represents a label selector requirement for cluster role aggregation.
/// </summary>
public class ClusterRoleLabelSelectorRequirement
{
  /// <summary>
  /// Gets or sets the key for the label selector.
  /// </summary>
  public required string Key { get; set; }

  /// <summary>
  /// Gets or sets the operator for the label selector.
  /// </summary>
  public required string Operator { get; set; }

  /// <summary>
  /// Gets or sets the values for the label selector.
  /// </summary>
  public IList<string>? Values { get; init; }
}