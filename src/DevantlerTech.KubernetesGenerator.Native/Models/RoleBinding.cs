namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a role binding for use with kubectl create rolebinding.
/// </summary>
public class RoleBinding(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the role binding.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the reference to the role or cluster role.
  /// </summary>
  public required RoleBindingRoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the role.
  /// </summary>
  public required IList<RoleBindingSubject> Subjects { get; init; }
}
