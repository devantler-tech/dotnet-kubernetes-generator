namespace Devantler.KubernetesGenerator.Configs.K3d.Models;

/// <summary>
/// Configuration options for Docker runtime
/// </summary>
public class K3dConfigOptionsRuntime
{
  /// <summary>
  /// GPU request for Docker runtime
  /// </summary>
  public string? GPURequest { get; set; }

  /// <summary>
  /// Labels to apply to Docker runtime container
  /// </summary>
  public IEnumerable<K3dConfigLabel>? Labels { get; set; }

  /// <summary>
  /// Ulimits to apply to Docker runtime
  /// </summary>
  public IEnumerable<K3dConfigOptionsRuntimeUlimit>? Ulimits { get; set; }
}
