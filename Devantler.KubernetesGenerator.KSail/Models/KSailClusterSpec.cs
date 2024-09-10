namespace Devantler.KubernetesGenerator.KSail.Models;

/// <summary>
/// The KSail cluster specification.
/// </summary>
public class KSailClusterSpec
{
  /// <summary>
  /// The Kubernetes distribution to use for the KSail cluster.
  /// </summary>
  public KSailKubernetesDistribution Distribution { get; set; } = KSailKubernetesDistribution.K3d;
}
