namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a NetworkPolicy.
/// </summary>
public class NetworkPolicySpec
{
  /// <summary>
  /// Gets or sets the pod selector for selecting pods that this policy applies to.
  /// </summary>
  public required LabelSelector PodSelector { get; init; }

  /// <summary>
  /// Gets or sets the policy types for this NetworkPolicy.
  /// </summary>
  public IReadOnlyList<NetworkPolicyType>? PolicyTypes { get; init; }

  /// <summary>
  /// Gets or sets the ingress rules for this NetworkPolicy.
  /// </summary>
  public IReadOnlyList<NetworkPolicyIngressRule>? Ingress { get; init; }

  /// <summary>
  /// Gets or sets the egress rules for this NetworkPolicy.
  /// </summary>
  public IList<NetworkPolicyEgressRule>? Egress { get; init; }
}