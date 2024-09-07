namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// Extra argument to pass to k3s
/// </summary>
public class K3dConfigOptionsK3sExtraArg
{
  /// <summary>
  /// Argument to pass to k3s
  /// </summary>
  public required string Arg { get; set; }

  /// <summary>
  /// Node filters to apply the argument to. (e.g. "agent:1")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
