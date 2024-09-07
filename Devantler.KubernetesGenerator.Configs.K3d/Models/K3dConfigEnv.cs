namespace Devantler.KubernetesGenerator.Configs.K3d.Models;

/// <summary>
/// An environment variable for a K3d cluster.
/// </summary>
public class K3dConfigEnv
{
  /// <summary>
  /// Key Value pair for the environment variable. (e.g. "key=value")
  /// </summary>
  public required string EnvVar { get; set; }

  /// <summary>
  /// The node filters for the environment variable. (e.g. "server:*" or "agent:0")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
