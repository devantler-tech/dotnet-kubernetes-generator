namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a port in a NetworkPolicy rule.
/// </summary>
public class NetworkPolicyPort
{
  /// <summary>
  /// Gets or sets the protocol for the port.
  /// </summary>
  public NetworkPolicyProtocol? Protocol { get; init; }

  /// <summary>
  /// Gets or sets the port number or name.
  /// </summary>
  public string? Port { get; init; }

  /// <summary>
  /// Gets or sets the end port for a port range.
  /// </summary>
  public int? EndPort { get; init; }
}
