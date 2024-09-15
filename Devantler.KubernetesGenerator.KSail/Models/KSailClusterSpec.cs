using Devantler.KubernetesGenerator.KSail.Models.Registry;

namespace Devantler.KubernetesGenerator.KSail.Models;

/// <summary>
/// The KSail cluster specification.
/// </summary>
public class KSailClusterSpec
{
  /// <summary>
  /// The Kubernetes distribution to use for the KSail cluster.
  /// </summary>
  public KSailKubernetesDistribution? Distribution { get; set; }

  /// <summary>
  /// The registries to create for the KSail cluster to reconcile flux artifacts, and to proxy and cache images.
  /// </summary>
  public IEnumerable<KSailRegistry>? Registries { get; set; }

  /// <summary>
  /// The GitOps tool to use for the KSail cluster.
  /// </summary>
  public KSailGitOpsTool? GitOpsTool { get; set; }
}
