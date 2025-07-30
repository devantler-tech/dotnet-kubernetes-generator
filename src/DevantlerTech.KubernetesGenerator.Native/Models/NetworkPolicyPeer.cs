namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a NetworkPolicy peer that defines traffic sources or destinations.
/// </summary>
public class NetworkPolicyPeer
{
  /// <summary>
  /// Gets or sets the pod selector for selecting pods.
  /// </summary>
  public LabelSelector? PodSelector { get; init; }

  /// <summary>
  /// Gets or sets the namespace selector for selecting namespaces.
  /// </summary>
  public LabelSelector? NamespaceSelector { get; init; }

  /// <summary>
  /// Gets or sets the IP block for IP-based rules.
  /// </summary>
  public NetworkPolicyIPBlock? IPBlock { get; init; }
}