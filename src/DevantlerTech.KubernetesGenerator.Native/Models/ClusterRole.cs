using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a cluster role for Kubernetes RBAC with type-safe options.
/// Uses BaseKubernetesGenerator since kubectl create clusterrole requires API server connectivity.
/// </summary>
public class ClusterRole
{
  /// <summary>
  /// Gets or sets the API version for the cluster role.
  /// </summary>
  [YamlMember(Alias = "apiVersion")]
  public string ApiVersion { get; set; } = "rbac.authorization.k8s.io/v1";

  /// <summary>
  /// Gets or sets the kind of the resource.
  /// </summary>
  [YamlMember(Alias = "kind")]
  public string Kind { get; set; } = "ClusterRole";

  /// <summary>
  /// Gets or sets the metadata for the cluster role.
  /// </summary>
  [YamlMember(Alias = "metadata")]
  public required ClusterRoleMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the rules that define the permissions.
  /// </summary>
  [YamlMember(Alias = "rules")]
  public IList<ClusterRoleRule>? Rules { get; init; }

  /// <summary>
  /// Gets or sets the aggregation rule for combining cluster roles.
  /// </summary>
  [YamlMember(Alias = "aggregationRule")]
  public ClusterRoleAggregationRule? AggregationRule { get; set; }
}