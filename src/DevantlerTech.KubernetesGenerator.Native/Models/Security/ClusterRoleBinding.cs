namespace DevantlerTech.KubernetesGenerator.Native.Models.Security;

/// <summary>
/// Represents a cluster role binding for use with kubectl create clusterrolebinding.
/// </summary>
public class ClusterRoleBinding
{
  /// <summary>
  /// Gets or sets the metadata for the cluster role binding.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the reference to the cluster role.
  /// </summary>
  public required RoleBindingRoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the cluster role.
  /// </summary>
  public required IList<RoleBindingSubject> Subjects { get; init; }
}
