namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

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
