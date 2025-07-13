namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the kind of role reference.
/// </summary>
public enum RoleBindingRoleRefKind
{
  /// <summary>
  /// A role reference.
  /// </summary>
  Role,

  /// <summary>
  /// A cluster role reference.
  /// </summary>
  ClusterRole
}