namespace DevantlerTech.KubernetesGenerator.Native.Models.RoleBinding;

/// <summary>
/// Represents a role binding for use with kubectl create rolebinding.
/// </summary>
public class NativeRoleBinding
{
  /// <summary>
  /// Gets or sets the metadata for the role binding.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the reference to the role or cluster role.
  /// </summary>
  public required NativeRoleBindingRoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the role.
  /// </summary>
  public required IList<NativeRoleBindingSubject> Subjects { get; init; }
}
