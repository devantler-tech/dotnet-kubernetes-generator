namespace DevantlerTech.KubernetesGenerator.Native.Models.ClusterRole;

/// <summary>
/// Represents a cluster role for Kubernetes RBAC with type-safe options.
/// Uses BaseKubernetesGenerator since kubectl create clusterrole requires API server connectivity.
/// </summary>
public class NativeClusterRole
{
  /// <summary>
  /// Gets or sets the API version for the cluster role.
  /// </summary>
  public string ApiVersion { get; set; } = "rbac.authorization.k8s.io/v1";

  /// <summary>
  /// Gets or sets the kind of the resource.
  /// </summary>
  public string Kind { get; set; } = "ClusterRole";

  /// <summary>
  /// Gets or sets the metadata for the cluster role.
  /// </summary>
  public required ClusterScopedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the rules that define the permissions.
  /// </summary>
  public IList<NativeClusterRoleRule>? Rules { get; init; }

  /// <summary>
  /// Gets or sets the aggregation rule for combining cluster roles.
  /// </summary>
  public NativeClusterRoleAggregationRule? AggregationRule { get; set; }
}
