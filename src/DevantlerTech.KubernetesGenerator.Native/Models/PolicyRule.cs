namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a policy rule for a Kubernetes Role.
/// </summary>
public class PolicyRule
{
  /// <summary>
  /// Gets or sets the verbs for the rule.
  /// </summary>
  public required IList<string> Verbs { get; init; }

  /// <summary>
  /// Gets or sets the resources for the rule.
  /// </summary>
  public required IList<string> Resources { get; init; }

  /// <summary>
  /// Gets or sets the API groups for the rule.
  /// </summary>
  public IList<string>? ApiGroups { get; init; }

  /// <summary>
  /// Gets or sets the resource names for the rule.
  /// </summary>
  public IList<string>? ResourceNames { get; init; }
}