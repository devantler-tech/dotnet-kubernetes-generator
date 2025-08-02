namespace DevantlerTech.KubernetesGenerator.Native.Models.PriorityClass;

/// <summary>
/// Represents a node selector term for persistent volume node affinity.
/// </summary>
public class NativeNodeSelectorTerm
{
  /// <summary>
  /// Gets or sets the match expressions.
  /// </summary>
  public IList<MatchExpression>? MatchExpressions { get; init; }
}
