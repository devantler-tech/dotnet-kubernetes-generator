namespace DevantlerTech.KubernetesGenerator.K3d.Models.Options;

/// <summary>
/// Configuration options for Kubeconfig
/// </summary>
public class K3dOptionsKubeconfig
{
  /// <summary>
  /// Add new cluster to your default Kubeconfig
  /// </summary>
  public bool UpdateDefaultKubeconfig { get; set; } = true;

  /// <summary>
  /// Set current-context to the new cluster's context
  /// </summary>
  public bool SwitchCurrentContext { get; set; } = true;
}
