namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

/// <summary>
/// Represents a reference to a role or cluster role.
/// </summary>
public class NativeRoleBindingRoleRef
{
  /// <summary>
  /// Gets or sets the kind of role reference (Role or ClusterRole).
  /// </summary>
  public required NativeRoleBindingRoleRefKind Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the role.
  /// </summary>
  public required string Name { get; set; }
}
