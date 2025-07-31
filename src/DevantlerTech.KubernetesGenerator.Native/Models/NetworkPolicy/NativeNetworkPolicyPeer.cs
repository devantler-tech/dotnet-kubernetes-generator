namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents a NetworkPolicy peer that defines traffic sources or destinations.
/// </summary>
public class NativeNetworkPolicyPeer
{
  /// <summary>
  /// Gets or sets the pod selector for selecting pods.
  /// </summary>
  public NativeLabelSelector? PodSelector { get; init; }

  /// <summary>
  /// Gets or sets the namespace selector for selecting namespaces.
  /// </summary>
  public NativeLabelSelector? NamespaceSelector { get; init; }

  /// <summary>
  /// Gets or sets the IP block for IP-based rules.
  /// </summary>
  public NativeNetworkPolicyIPBlock? IPBlock { get; init; }
}
