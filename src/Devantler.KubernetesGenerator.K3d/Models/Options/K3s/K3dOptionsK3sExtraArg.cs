namespace Devantler.KubernetesGenerator.K3d.Models.Options.K3s;

/// <summary>
/// Extra argument to pass to k3s
/// </summary>
public class K3dOptionsK3sExtraArg
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
