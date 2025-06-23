namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options.Runtime;

/// <summary>
/// Configuration options for Docker runtime
/// </summary>
public class K3dOptionsRuntime
{
  /// <summary>
  /// GPU request for Docker runtime
  /// </summary>
  public string? GPURequest { get; set; }

  /// <summary>
  /// Labels to apply to Docker runtime container
  /// </summary>
  public IEnumerable<K3dOptionsLabel>? Labels { get; set; }

  /// <summary>
  /// Ulimits to apply to Docker runtime
  /// </summary>
  public IEnumerable<K3dOptionsRuntimeUlimit>? Ulimits { get; set; }
}
