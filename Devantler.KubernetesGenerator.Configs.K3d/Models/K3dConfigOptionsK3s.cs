namespace Devantler.KubernetesGenerator.Configs.K3d.Models;

/// <summary>
/// Configuration options for k3s
/// </summary>
public class K3dConfigOptionsK3s
{
  /// <summary>
  /// Extra arguments to pass to k3s
  /// </summary>
  public IEnumerable<K3dConfigOptionsK3sExtraArg>? ExtraArgs { get; set; }

  /// <summary>
  /// Node labels to apply to k3s nodes
  /// </summary>
  public IEnumerable<K3dConfigLabel>? NodeLabels { get; set; }
}
