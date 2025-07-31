namespace DevantlerTech.KubernetesGenerator.Native.Models.Networking;

/// <summary>
/// Represents an egress rule in a NetworkPolicy.
/// </summary>
public class NetworkPolicyEgressRule
{
  /// <summary>
  /// Gets or sets the ports that are allowed by this rule.
  /// </summary>
  public IReadOnlyList<NetworkPolicyPort>? Ports { get; init; }

  /// <summary>
  /// Gets or sets the destinations which can be reached by this rule.
  /// </summary>
  public IReadOnlyList<NetworkPolicyPeer>? To { get; init; }
}
