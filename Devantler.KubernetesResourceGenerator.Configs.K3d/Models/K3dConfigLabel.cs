namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// Node label to apply to k3s nodes
/// </summary>
public class K3dConfigLabel
{
  /// <summary>
  /// Key Value pair for the label
  /// </summary>
  public required string Label { get; set; }

  /// <summary>
  /// Node filter to apply the label to. (e.g. "agent:1")
  /// </summary>
  public IEnumerable<string>? NodeFilters { get; set; }
}
