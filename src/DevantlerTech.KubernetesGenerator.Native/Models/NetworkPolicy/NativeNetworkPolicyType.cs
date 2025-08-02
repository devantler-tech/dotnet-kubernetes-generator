namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Enum for supported NetworkPolicy types.
/// </summary>
public enum NativeNetworkPolicyType
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
