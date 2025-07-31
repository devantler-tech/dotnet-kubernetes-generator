namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Enum for supported NetworkPolicy types.
/// </summary>
public enum NetworkPolicyType
{
  /// <summary>
  /// Ingress policy type - controls incoming traffic to pods
  /// </summary>
  Ingress,
  /// <summary>
  /// Egress policy type - controls outgoing traffic from pods
  /// </summary>
  Egress
}
