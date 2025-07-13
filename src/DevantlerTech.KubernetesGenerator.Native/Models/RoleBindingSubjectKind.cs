namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the kind of subject.
/// </summary>
public enum RoleBindingSubjectKind
{
  /// <summary>
  /// A user subject.
  /// </summary>
  User,

  /// <summary>
  /// A group subject.
  /// </summary>
  Group,

  /// <summary>
  /// A service account subject.
  /// </summary>
  ServiceAccount
}