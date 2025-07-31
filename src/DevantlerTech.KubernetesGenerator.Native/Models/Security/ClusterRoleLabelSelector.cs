namespace DevantlerTech.KubernetesGenerator.Native.Models.Security;

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
  public IList<MatchExpression>? MatchExpressions { get; init; }
}
