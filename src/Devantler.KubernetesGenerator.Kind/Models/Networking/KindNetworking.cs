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
  public KindNetworkingIPFamily? IPFamily { get; set; }

  /// <summary>
  /// The IP address to use for the Kind cluster's Kubernetes API server.
  /// </summary>
  public string? ApiServerAddress { get; set; }

  /// <summary>
  /// The port to use for the Kind cluster's Kubernetes API server.
  /// </summary>
  public int? ApiServerPort { get; set; }

  /// <summary>
  /// The subnet to use for the Kind cluster's pods.
  /// </summary>
  public string? PodSubnet { get; set; }

  /// <summary>
  /// The subnet to use for the Kind cluster's services.
  /// </summary>
  public string? ServiceSubnet { get; set; }

  /// <summary>
  /// Whether to disable or enable the default CNI.
  /// </summary>
  public bool? DisableDefaultCNI { get; set; }

  /// <summary>
  /// The mode to use for the kube-proxy.
  /// </summary>
  public KindNetworkingKubeProxyMode? KubeProxyMode { get; set; }
}
