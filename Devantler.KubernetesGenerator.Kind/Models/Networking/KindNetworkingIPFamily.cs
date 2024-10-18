using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.Kind.Models.Networking;

/// <summary>
/// The IP families supported by Kind.
/// </summary>
public enum KindNetworkingIPFamily
{
  /// <summary>
  /// Use IPv4 for the Kind cluster.
  /// </summary>
  [EnumMember(Value = "ipv4")]
  IPv4,

  /// <summary>
  /// Use IPv6 for the Kind cluster.
  /// </summary>
  [EnumMember(Value = "ipv6")]
  IPv6,

  /// <summary>
  /// Use both IPv4 and IPv6 for the Kind cluster. Requires Kind 0.11+, on Kubernetes 1.20+.
  /// </summary>
  Dual
}
