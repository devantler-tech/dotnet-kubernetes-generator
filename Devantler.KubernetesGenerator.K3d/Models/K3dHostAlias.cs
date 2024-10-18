namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// A host alias for a K3d cluster.
/// </summary>
public class K3dHostAlias
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
