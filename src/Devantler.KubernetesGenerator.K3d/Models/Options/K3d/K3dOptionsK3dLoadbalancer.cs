namespace Devantler.KubernetesGenerator.K3d.Models.Options.K3d;

/// <summary>
/// Configuration for the loadbalancer
/// </summary>
public class K3dOptionsK3dLoadbalancer
{
  /// <summary>
  /// Configuration overrides for the loadbalancer
  /// </summary>
  public IEnumerable<string>? ConfigOverrides { get; set; }
}
