namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// A host alias for a K3d cluster.
/// </summary>
public class K3dConfigHostAlias
{
  /// <summary>
  /// IP for the host alias.
  /// </summary>
  public required string Ip { get; set; }

  /// <summary>
  /// Hostnames for the host alias.
  /// </summary>
  public required IEnumerable<string> Hostnames { get; set; }
}
