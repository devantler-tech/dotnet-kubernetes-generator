namespace DevantlerTech.KubernetesGenerator.K3d.Models;

/// <summary>
/// A configuration for a K3d volume.
/// </summary>
public class K3dVolume
{
  /// <summary>
  /// Volume path mapping. (e.g. /host/path:/node/path)
  /// </summary>
  public required string Volume { get; set; }

  /// <summary>
  /// Node filters for the volume. (e.g. "server:*" or "agent:0")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
