namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a label selector for matching pods.
/// </summary>
public class LabelSelector
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