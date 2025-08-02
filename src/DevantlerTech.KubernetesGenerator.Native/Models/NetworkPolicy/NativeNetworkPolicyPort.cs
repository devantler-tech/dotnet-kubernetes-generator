namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents a port in a NetworkPolicy rule.
/// </summary>
public class NativeNetworkPolicyPort
{
  /// <summary>
  /// Gets or sets the protocol for the port.
  /// </summary>
  public NativeNetworkPolicyProtocol? Protocol { get; init; }

  /// <summary>
  /// Gets or sets the port number or name.
  /// </summary>
  public string? Port { get; init; }

  /// <summary>
  /// Gets or sets the end port for a port range.
  /// </summary>
  public int? EndPort { get; init; }
}
