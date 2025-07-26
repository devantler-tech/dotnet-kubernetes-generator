namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a node selector term for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeAffinityNodeSelectorTerm
{
  /// <summary>
  /// Gets or sets the match expressions.
  /// </summary>
  public IList<MatchExpression>? MatchExpressions { get; init; }
}
