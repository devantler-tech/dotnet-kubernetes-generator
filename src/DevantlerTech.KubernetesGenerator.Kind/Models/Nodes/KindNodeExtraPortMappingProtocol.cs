namespace DevantlerTech.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// The protocol for an extra port mapping on a Kind node.
/// </summary>
public enum KindNodeExtraPortMappingProtocol
{
  /// <summary>
  /// TCP protocol.
  /// </summary>
  TCP,

  /// <summary>
  /// UDP protocol.
  /// </summary>
  UDP,

  /// <summary>
  /// SCTP protocol.
  /// </summary>
  SCTP
}
