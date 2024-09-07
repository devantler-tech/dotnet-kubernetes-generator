namespace Devantler.KubernetesGenerator.Configs.K3d.Models;

/// <summary>
/// Metadata for a K3d cluster.
/// </summary>
public class K3dConfigMetadata
{
  /// <summary>
  /// The name of the cluster.
  /// </summary>
  public required string Name { get; set; }
}
