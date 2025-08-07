namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a node selector requirement for persistent volume node affinity.
/// </summary>
public class MatchExpression
{
  /// <summary>
  /// Gets or sets the key.
  /// </summary>
  public required string Key { get; set; }

  /// <summary>
  /// Gets or sets the operator.
  /// </summary>
  public required MatchExpressionOperator Operator { get; set; }

  /// <summary>
  /// Gets or sets the values.
  /// </summary>
  public IList<string>? Values { get; init; }
}
