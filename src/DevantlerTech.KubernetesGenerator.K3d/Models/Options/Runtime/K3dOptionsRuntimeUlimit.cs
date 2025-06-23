namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options.Runtime;

/// <summary>
/// Ulimit to apply to Docker runtime
/// </summary>
public class K3dOptionsRuntimeUlimit
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
