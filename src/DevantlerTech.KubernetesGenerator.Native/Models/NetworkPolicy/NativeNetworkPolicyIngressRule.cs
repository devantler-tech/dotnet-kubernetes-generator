namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents an ingress rule in a NetworkPolicy.
/// </summary>
public class NativeNetworkPolicyIngressRule
{
  /// <summary>
  /// Gets or sets the ports that are allowed by this rule.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyPort>? Ports { get; init; }

  /// <summary>
  /// Gets or sets the sources which can access the selected pods.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyPeer>? From { get; init; }
}
