namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Enum for supported container port protocols.
/// </summary>
public enum PodContainerPortProtocol
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
