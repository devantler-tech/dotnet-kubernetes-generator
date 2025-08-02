using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models.Role;

/// <summary>
/// Represents a role for Kubernetes RBAC with type-safe options.
/// Uses KubernetesGenerator to generate Kubernetes RBAC roles programmatically.
/// </summary>
public class NativeRole
{
  /// <summary>
  /// Gets or sets the API version for the role.
  /// </summary>
  public string ApiVersion { get; set; } = "rbac.authorization.k8s.io/v1";

  /// <summary>
  /// Gets or sets the kind of the resource.
  /// </summary>
  public string Kind { get; set; } = "Role";

  /// <summary>
  /// Gets or sets the metadata for the role.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the rules that define the permissions.
  /// </summary>
  public IList<NativeRoleRule>? Rules { get; init; }
}
