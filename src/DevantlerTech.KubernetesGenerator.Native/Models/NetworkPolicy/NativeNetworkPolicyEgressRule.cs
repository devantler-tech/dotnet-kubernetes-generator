namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents an egress rule in a NetworkPolicy.
/// </summary>
public class NativeNetworkPolicyEgressRule
{
  /// <summary>
  /// Gets or sets the ports that are allowed by this rule.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyPort>? Ports { get; init; }

  /// <summary>
  /// Gets or sets the destinations which can be reached by this rule.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyPeer>? To { get; init; }
}
