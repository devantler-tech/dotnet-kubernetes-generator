namespace DevantlerTech.KubernetesGenerator.Native.Models.Role;

/// <summary>
/// Represents a rule for role permissions with type-safe verbs.
/// </summary>
public class NativeRoleRule
{
  /// <summary>
  /// Gets or sets the verbs that apply to the resources.
  /// </summary>
  public required IList<NativeRoleVerb> Verbs { get; init; } = [];

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
}