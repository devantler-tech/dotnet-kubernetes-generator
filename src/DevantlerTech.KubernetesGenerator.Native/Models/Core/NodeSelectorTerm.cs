namespace DevantlerTech.KubernetesGenerator.Native.Models.Core;

/// <summary>
/// Represents a node selector term for persistent volume node affinity.
/// </summary>
public class NodeSelectorTerm
{
  /// <summary>
  /// Gets or sets the match expressions.
  /// </summary>
  public IList<MatchExpression>? MatchExpressions { get; init; }
}
