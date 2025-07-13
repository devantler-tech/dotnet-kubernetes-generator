using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a ResourceQuota for use with kubectl create quota commands.
/// </summary>
public class ResourceQuota
{
  /// <summary>
  /// Gets or sets the name of the resource quota.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace for the resource quota.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// Gets or sets the hard resource limits for the resource quota.
  /// </summary>
  public Dictionary<string, ResourceQuantity>? Hard { get; init; }

  /// <summary>
  /// Gets or sets the scopes for the resource quota.
  /// </summary>
  public IList<string>? Scopes { get; init; }
}