namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Enum for supported NetworkPolicy port protocols.
/// </summary>
public enum NetworkPolicyProtocol
{
  /// <summary>
  /// Transmission Control Protocol
  /// </summary>
  TCP,
  /// <summary>
  /// User Datagram Protocol
  /// </summary>
  UDP,
  /// <summary>
  /// Stream Control Transmission Protocol
  /// </summary>
  SCTP
}