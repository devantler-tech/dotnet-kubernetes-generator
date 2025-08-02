namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

/// <summary>
/// Represents a cluster role binding for use with kubectl create clusterrolebinding.
/// </summary>
public class NativeClusterRoleBinding
{
  /// <summary>
  /// Gets or sets the metadata for the cluster role binding.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the reference to the cluster role.
  /// </summary>
  public required NativeRoleBindingRoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the cluster role.
  /// </summary>
  public required IList<NativeRoleBindingSubject> Subjects { get; init; }
}
