namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

/// <summary>
/// Represents the kind of subject.
/// </summary>
public enum NativeRoleBindingSubjectKind
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
