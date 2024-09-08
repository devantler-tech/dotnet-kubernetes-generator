namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Configuration options for k3d
/// </summary>
public class K3dConfigOptionsK3d
{
  /// <summary>
  /// Wait for cluster to be usable before returning
  /// </summary>
  public bool Wait { get; set; } = true;

  /// <summary>
  /// Wait timeout before aborting
  /// </summary>
  public string? Timeout { get; set; }

  /// <summary>
  /// Disable loadbalancer
  /// </summary>
  public bool? DisableLoadbalancer { get; set; }

  /// <summary>
  /// Disable image volume
  /// </summary>
  public bool? DisableImageVolume { get; set; }

  /// Disable rollback
  /// <summary>
  /// </summary>
  public bool? DisableRollback { get; set; }

  /// <summary>
  /// Configuration for the loadbalancer
  /// </summary>
  public K3dConfigOptionsK3dLoadbalancer? Loadbalancer { get; set; }
}
