using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Role for use with kubectl create role.
/// </summary>
public class Role
{
  /// <summary>
  /// Gets or sets the metadata for the role.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the rules for the role.
  /// </summary>
  public required IList<PolicyRule> Rules { get; init; }
}