using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a ResourceQuota for use with kubectl create quota commands.
/// </summary>
public class ResourceQuota(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the resource quota.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the hard resource limits for the resource quota.
  /// </summary>
  public Dictionary<string, ResourceQuantity>? Hard { get; init; }

  /// <summary>
  /// Gets or sets the scopes for the resource quota.
  /// </summary>
  public IList<string>? Scopes { get; init; }
}
