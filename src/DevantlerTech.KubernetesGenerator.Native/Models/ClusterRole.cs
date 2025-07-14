namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a cluster role for use with kubectl create clusterrole.
/// </summary>
public class ClusterRole(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the cluster role.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the verbs that apply to the resources contained in the rule.
  /// </summary>
  public IList<string>? Verbs { get; init; }

  /// <summary>
  /// Gets or sets the resources that the rule applies to.
  /// </summary>
  public IList<string>? Resources { get; init; }

  /// <summary>
  /// Gets or sets the resource names in the white list that the rule applies to.
  /// </summary>
  public IList<string>? ResourceNames { get; init; }

  /// <summary>
  /// Gets or sets the partial URLs that user should have access to.
  /// </summary>
  public IList<string>? NonResourceUrls { get; init; }

  /// <summary>
  /// Gets or sets the aggregation label selector for combining ClusterRoles.
  /// </summary>
  public string? AggregationRule { get; init; }
}