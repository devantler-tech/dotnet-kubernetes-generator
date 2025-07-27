namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a rule for cluster role permissions with type-safe verbs.
/// </summary>
public class ClusterRoleRule
{
  /// <summary>
  /// Gets or sets the verbs that apply to the resources.
  /// </summary>
  public required IList<ClusterRoleVerb> Verbs { get; init; } = [];

  /// <summary>
  /// Gets or sets the API groups that the rule applies to.
  /// Empty string "" means the core API group.
  /// </summary>
  public IList<string>? ApiGroups { get; init; }

  /// <summary>
  /// Gets or sets the resources that the rule applies to.
  /// </summary>
  public IList<string>? Resources { get; init; }

  /// <summary>
  /// Gets or sets specific resource names that the rule applies to.
  /// If empty, applies to all resources of the specified type.
  /// </summary>
  public IList<string>? ResourceNames { get; init; }

  /// <summary>
  /// Gets or sets non-resource URLs that the rule applies to.
  /// Used for cluster-scoped permissions on non-resource endpoints.
  /// </summary>
  public IList<string>? NonResourceURLs { get; init; }
}