namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// Configuration options for k3d and k3s
/// </summary>
public class K3dConfigOptions
{
  /// <summary>
  /// Options for k3d
  /// </summary>
  public K3dConfigOptionsK3d? K3d { get; set; }

  /// <summary>
  /// Options for k3s
  /// </summary>
  public K3dConfigOptionsK3s? K3s { get; set; }

  /// <summary>
  /// Options for Kubeconfig
  /// </summary>
  public K3dConfigOptionsKubeconfig? Kubeconfig { get; set; }

  /// <summary>
  /// Options for Docker runtime
  /// </summary>
  public K3dConfigOptionsRuntime? Runtime { get; set; }
}
