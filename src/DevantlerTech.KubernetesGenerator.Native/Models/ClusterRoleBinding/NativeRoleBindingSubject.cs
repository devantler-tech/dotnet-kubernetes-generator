namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

/// <summary>
/// Represents a subject that can be bound to a role.
/// </summary>
public class NativeRoleBindingSubject
{
  /// <summary>
  /// Gets or sets the kind of subject (User, Group, or ServiceAccount).
  /// </summary>
  public required NativeRoleBindingSubjectKind Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the subject.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the subject (optional, only applicable for ServiceAccount).
  /// </summary>
  public string? Namespace { get; set; }
}
