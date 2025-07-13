using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a ClusterRoleBinding for use with kubectl create clusterrolebinding.
/// </summary>
public class ClusterRoleBinding
{
  /// <summary>
  /// Gets or sets the metadata for the cluster role binding.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the cluster role name to bind to.
  /// </summary>
  public required string ClusterRole { get; set; }

  /// <summary>
  /// Gets the subjects to bind to the cluster role.
  /// </summary>
  public IList<Subject>? Subjects { get; init; }
}