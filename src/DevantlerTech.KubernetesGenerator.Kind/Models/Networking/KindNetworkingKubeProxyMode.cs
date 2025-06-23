using System.Runtime.Serialization;

namespace DevantlerTech.KubernetesGenerator.Kind.Models.Networking;

/// <summary>
/// Supported kube-proxy modes for Kind.
/// </summary>
public enum KindNetworkingKubeProxyMode
{
  /// <summary>
  /// Use IPTables for the kube-proxy.
  /// </summary>
  [EnumMember(Value = "iptables")]
  IPTables,

  /// <summary>
  /// Use nftables for the kube-proxy.
  /// </summary>
  [EnumMember(Value = "nftables")]
  NFTables,

  /// <summary>
  /// Use IPVS for the kube-proxy.
  /// </summary>
  [EnumMember(Value = "ipvs")]
  IPVS,

  /// <summary>
  /// Do not use kube-proxy.
  /// </summary>
  None

}
