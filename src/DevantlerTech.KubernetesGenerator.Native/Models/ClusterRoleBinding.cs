namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a cluster role binding for use with kubectl create clusterrolebinding.
/// </summary>
public class ClusterRoleBinding(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the cluster role binding.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the reference to the cluster role.
  /// </summary>
  public required RoleBindingRoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the cluster role.
  /// </summary>
  public required IList<RoleBindingSubject> Subjects { get; init; }
}