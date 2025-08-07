using DevantlerTech.KubernetesGenerator.Core.Models;
namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents the specification for a NetworkPolicy.
/// </summary>
public class NativeNetworkPolicySpec
{
  /// <summary>
  /// Gets or sets the pod selector for selecting pods that this policy applies to.
  /// </summary>
  public required LabelSelector PodSelector { get; init; }

  /// <summary>
  /// Gets or sets the policy types for this NetworkPolicy.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyType>? PolicyTypes { get; init; }

  /// <summary>
  /// Gets or sets the ingress rules for this NetworkPolicy.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyIngressRule>? Ingress { get; init; }

  /// <summary>
  /// Gets or sets the egress rules for this NetworkPolicy.
  /// </summary>
  public IReadOnlyList<NativeNetworkPolicyEgressRule>? Egress { get; init; }
}
