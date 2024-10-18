namespace Devantler.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// An extra port mapping to add to a Kind node.
/// </summary>
public class KindNodeExtraPortMapping
{
  /// <summary>
  /// The port on the host to map.
  /// </summary>
  public required int HostPort { get; set; }

  /// <summary>
  /// The port in the container to map the host port to.
  /// </summary>
  public required int ContainerPort { get; set; }

  /// <summary>
  /// The address to bind to on the host.
  /// </summary>
  public string ListenAddress { get; set; } = "0.0.0.0";

  /// <summary>
  /// The protocol to use for the mapping.
  /// </summary>
  public KindNodeExtraPortMappingProtocol Protocol { get; set; } = KindNodeExtraPortMappingProtocol.TCP;
}
