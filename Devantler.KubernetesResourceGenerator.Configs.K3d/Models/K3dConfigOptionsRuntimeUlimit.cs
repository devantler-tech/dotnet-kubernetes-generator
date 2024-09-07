namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// Ulimit to apply to Docker runtime
/// </summary>
public class K3dConfigOptionsRuntimeUlimit
{
  /// <summary>
  /// Name of the ulimit
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Soft limit
  /// </summary>
  public int? Soft { get; set; }

  /// <summary>
  /// Hard limit
  /// </summary>
  public int? Hard { get; set; }
}
