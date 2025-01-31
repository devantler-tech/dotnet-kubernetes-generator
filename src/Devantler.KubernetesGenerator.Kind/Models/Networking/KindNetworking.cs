using System.Text.Json.Serialization;

namespace Devantler.KubernetesGenerator.Kind.Models.Networking;

/// <summary>
/// Network related configurations for the Kind cluster.
/// </summary>
public class KindNetworking
{
  /// <summary>
  /// The IP family to use for the Kind cluster.
  /// </summary>
  [JsonPropertyName("ipFamily")]
  public KindNetworkingIPFamily IPFamily { get; set; } = KindNetworkingIPFamily.IPv4;

  /// <summary>
  /// The IP address to use for the Kind cluster's Kubernetes API server.
  /// </summary>
  public string ApiServerAddress { get; set; } = "127.0.0.1";

  /// <summary>
  /// The port to use for the Kind cluster's Kubernetes API server.
  /// </summary>
  public int ApiServerPort { get; set; } = 6443;

  /// <summary>
  /// The subnet to use for the Kind cluster's pods.
  /// </summary>
  public string PodSubnet { get; set; } = "10.244.0.0/16";

  /// <summary>
  /// The subnet to use for the Kind cluster's services.
  /// </summary>
  public string ServiceSubnet { get; set; } = "10.96.0.0/16";

  /// <summary>
  /// Whether to disable or enable the default CNI.
  /// </summary>
  public bool DisableDefaultCNI { get; set; }

  /// <summary>
  /// The mode to use for the kube-proxy.
  /// </summary>
  public KindNetworkingKubeProxyMode KubeProxyMode { get; set; } = KindNetworkingKubeProxyMode.IPTables;
}
