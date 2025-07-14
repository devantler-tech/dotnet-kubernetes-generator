namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a policy rule for a role.
/// </summary>
public class RolePolicyRule
{
  /// <summary>
  /// Gets or sets the list of verbs that can be performed on the resources.
  /// </summary>
  public required IList<string> Verbs { get; init; }

  /// <summary>
  /// Gets or sets the list of resources that the rule applies to.
  /// </summary>
  public required IList<string> Resources { get; init; }

  /// <summary>
  /// Gets or sets the list of API groups that the rule applies to.
  /// </summary>
  public IList<string>? ApiGroups { get; init; }

  /// <summary>
  /// Gets or sets the list of resource names that the rule applies to.
  /// </summary>
  public IList<string>? ResourceNames { get; init; }

  /// <summary>
  /// Gets or sets the list of non-resource URLs that the rule applies to.
  /// </summary>
  public IList<string>? NonResourceURLs { get; init; }
}