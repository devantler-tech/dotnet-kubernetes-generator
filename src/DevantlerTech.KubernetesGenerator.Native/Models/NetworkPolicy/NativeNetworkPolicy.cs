using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models.NetworkPolicy;

/// <summary>
/// Represents a Kubernetes NetworkPolicy resource.
/// </summary>
public class NativeNetworkPolicy
{
  /// <summary>
  /// Gets or sets the API version of this NetworkPolicy.
  /// </summary>
  public string ApiVersion { get; set; } = "networking.k8s.io/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "NetworkPolicy";

  /// <summary>
  /// Gets or sets the metadata for the NetworkPolicy.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the NetworkPolicy.
  /// </summary>
  public required NativeNetworkPolicySpec Spec { get; set; }
}
