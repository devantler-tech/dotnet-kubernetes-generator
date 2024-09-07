namespace Devantler.KubernetesGenerator.Configs.K3d.Models;

/// <summary>
/// Configuration for the loadbalancer
/// </summary>
public class K3dConfigOptionsK3dLoadbalancer
{
  /// <summary>
  /// Configuration overrides for the loadbalancer
  /// </summary>
  public IEnumerable<string>? ConfigOverrides { get; set; }
}
