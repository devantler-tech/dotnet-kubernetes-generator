namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Configuration for a port mapping.
/// </summary>
public class K3dPort
{
  /// <summary>
  /// The port mapping. (e.g. 8080:80)
  /// </summary>
  public required string Port { get; set; }

  /// <summary>
  /// Node filters for the port mapping. (e.g. "server:*" or "agent:0")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
