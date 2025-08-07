using DevantlerTech.KubernetesGenerator.Core.Models;
namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRole;

/// <summary>
/// Represents a label selector for cluster role aggregation.
/// </summary>
public class NativeClusterRoleLabelSelector
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
