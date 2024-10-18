using Devantler.KubernetesGenerator.Kind.Models.Networking;
using Devantler.KubernetesGenerator.Kind.Models.Nodes;

namespace Devantler.KubernetesGenerator.Kind.Models;

/// <summary>
/// A configuration for a Kind cluster.
/// </summary>
public class KindConfig
{
  /// <summary>
  /// API Kind for the Kind cluster.
  /// </summary>
  public string Kind { get; set; } = "Cluster";

  /// <summary>
  /// API version for the Kind cluster.
  /// </summary>
  public string ApiVersion { get; set; } = "kind.x-k8s.io/v1alpha4";

  /// <summary>
  /// Name of the Kind cluster.
  /// </summary>
  public string Name { get; set; } = "kind-default";

  /// <summary>
  /// A set of Kubernetes features that can be enabled or disabled.
  /// </summary>
  public KindFeatureGates? FeatureGates { get; set; }

  /// <summary>
  /// A set of runtime configurations for the Kind cluster.
  /// </summary>
  public Dictionary<string, string> RuntimeConfig { get; } = [];

  /// <summary>
  /// Network related configurations for the Kind cluster.
  /// </summary>
  public KindNetworking? Networking { get; set; }

  /// <summary>
  /// Nodes in the Kind cluster.
  /// </summary>
  public IEnumerable<KindNode>? Nodes { get; set; }

  /// <summary>
  /// A set of patches to apply to the containerd configuration.
  /// </summary>
  public IEnumerable<string> ContainerdConfigPatches { get; set; } = [
    """
    [plugins."io.containerd.grpc.v1.cri".registry]
      config_path = "/etc/containerd/certs.d"
    """
  ];
}
