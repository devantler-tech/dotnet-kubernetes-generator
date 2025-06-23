using DevantlerTech.KubernetesGenerator.K3d.Models.Options.K3d;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options.K3s;
using DevantlerTech.KubernetesGenerator.K3d.Models.Options.Runtime;

namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options;

/// <summary>
/// Configuration options for k3d and k3s
/// </summary>
public class K3dOptions
{
  /// <summary>
  /// Options for k3d
  /// </summary>
  public K3dOptionsK3d? K3d { get; set; }

  /// <summary>
  /// Options for k3s
  /// </summary>
  public K3dOptionsK3s? K3s { get; set; }

  /// <summary>
  /// Options for Kubeconfig
  /// </summary>
  public K3dOptionsKubeconfig? Kubeconfig { get; set; }

  /// <summary>
  /// Options for Docker runtime
  /// </summary>
  public K3dOptionsRuntime? Runtime { get; set; }
}
