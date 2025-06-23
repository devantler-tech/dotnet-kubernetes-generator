namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options;

/// <summary>
/// A label to apply to nodes or the runtime
/// </summary>
public class K3dOptionsLabel
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
