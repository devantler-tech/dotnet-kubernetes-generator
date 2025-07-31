
namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents a port to expose from a container.
/// </summary>

public class NativePodContainerPort
{
  /// <summary>
  /// Gets or sets the name of this port within the service.
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Gets or sets the number of port to expose on the pod's IP address.
  /// </summary>
  public required int ContainerPort { get; set; }

  /// <summary>
  /// Gets or sets the protocol for this port.
  /// </summary>
  public NativePodContainerPortProtocol? Protocol { get; set; }

  /// <summary>
  /// Gets or sets the number of port to expose on the host.
  /// </summary>
  public int? HostPort { get; set; }

  /// <summary>
  /// Gets or sets the host IP to bind the external port to.
  /// </summary>
  public string? HostIP { get; set; }
}
