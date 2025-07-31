namespace DevantlerTech.KubernetesGenerator.Native.Models.Networking;

/// <summary>
/// Represents an ingress rule in a NetworkPolicy.
/// </summary>
public class NetworkPolicyIngressRule
{
  /// <summary>
  /// Gets or sets the ports that are allowed by this rule.
  /// </summary>
  public IReadOnlyList<NetworkPolicyPort>? Ports { get; init; }

  /// <summary>
  /// Gets or sets the sources which can access the selected pods.
  /// </summary>
  public IReadOnlyList<NetworkPolicyPeer>? From { get; init; }
}
