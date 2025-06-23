namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options.K3s;

/// <summary>
/// Configuration options for k3s
/// </summary>
public class K3dOptionsK3s
{
  /// <summary>
  /// Extra arguments to pass to k3s
  /// </summary>
  public IEnumerable<K3dOptionsK3sExtraArg>? ExtraArgs { get; set; }

  /// <summary>
  /// Node labels to apply to k3s nodes
  /// </summary>
  public IEnumerable<K3dOptionsLabel>? NodeLabels { get; set; }
}
