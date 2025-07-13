using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a NetworkPolicy for use with NetworkPolicyGenerator.
/// </summary>
public class NetworkPolicy
{
  /// <summary>
  /// Gets or sets the metadata for the NetworkPolicy.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the pod selector for the NetworkPolicy.
  /// This field is required by Kubernetes.
  /// </summary>
  public required V1LabelSelector PodSelector { get; set; }

  /// <summary>
  /// Gets the ingress rules for the NetworkPolicy.
  /// This field is optional.
  /// </summary>
  public IList<V1NetworkPolicyIngressRule>? Ingress { get; init; }

  /// <summary>
  /// Gets the egress rules for the NetworkPolicy.
  /// This field is optional.
  /// </summary>
  public IList<V1NetworkPolicyEgressRule>? Egress { get; init; }

  /// <summary>
  /// Gets the policy types for the NetworkPolicy.
  /// This field is optional - if not specified, Kubernetes will infer from the presence of Ingress/Egress rules.
  /// </summary>
  public IList<string>? PolicyTypes { get; init; }
}