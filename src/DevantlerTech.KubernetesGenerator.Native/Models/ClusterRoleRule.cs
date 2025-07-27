using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a rule for cluster role permissions with type-safe verbs.
/// </summary>
public class ClusterRoleRule
{
  /// <summary>
  /// Gets or sets the verbs that apply to the resources.
  /// </summary>
  [YamlMember(Alias = "verbs")]
  public required IList<string> Verbs { get; init; } = [];

  /// <summary>
  /// Gets or sets the API groups that the rule applies to.
  /// Empty string "" means the core API group.
  /// </summary>
  [YamlMember(Alias = "apiGroups")]
  public IList<string>? ApiGroups { get; init; }

  /// <summary>
  /// Gets or sets the resources that the rule applies to.
  /// </summary>
  [YamlMember(Alias = "resources")]
  public IList<string>? Resources { get; init; }

  /// <summary>
  /// Gets or sets specific resource names that the rule applies to.
  /// If empty, applies to all resources of the specified type.
  /// </summary>
  [YamlMember(Alias = "resourceNames")]
  public IList<string>? ResourceNames { get; init; }

  /// <summary>
  /// Gets or sets non-resource URLs that the rule applies to.
  /// Used for cluster-scoped permissions on non-resource endpoints.
  /// </summary>
  [YamlMember(Alias = "nonResourceURLs")]
  public IList<string>? NonResourceURLs { get; init; }
}

/// <summary>
/// Helper class for creating cluster role rules with type-safe verbs.
/// </summary>
public static class ClusterRoleRuleBuilder
{
  /// <summary>
  /// Converts ClusterRoleVerb enums to their string representations.
  /// </summary>
  /// <param name="verbs">The verbs to convert.</param>
  /// <returns>String representations of the verbs.</returns>
  public static IList<string> ConvertVerbs(params ClusterRoleVerb[] verbs) =>
    [.. verbs.Select(ConvertVerbToString)];

  /// <summary>
  /// Converts a ClusterRoleVerb enum to its string representation.
  /// </summary>
  /// <param name="verb">The verb to convert.</param>
  /// <returns>The string representation of the verb.</returns>
  public static string ConvertVerbToString(ClusterRoleVerb verb) => verb switch
  {
    ClusterRoleVerb.Get => "get",
    ClusterRoleVerb.List => "list",
    ClusterRoleVerb.Watch => "watch",
    ClusterRoleVerb.Create => "create",
    ClusterRoleVerb.Update => "update",
    ClusterRoleVerb.Patch => "patch",
    ClusterRoleVerb.Delete => "delete",
    ClusterRoleVerb.DeleteCollection => "deletecollection",
    ClusterRoleVerb.Bind => "bind",
    ClusterRoleVerb.Escalate => "escalate",
    ClusterRoleVerb.All => "*",
    _ => throw new ArgumentException($"Unsupported verb: {verb}", nameof(verb))
  };
}