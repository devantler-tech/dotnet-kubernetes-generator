using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.CertManager.Models;

/// <summary>
/// Represents a Cert Manager ClusterIssuer.
/// </summary>
public class CertManagerClusterIssuer
{
  /// <summary>
  /// Gets the API version.
  /// </summary>
  public string ApiVersion { get; } = "cert-manager.io/v1";

  /// <summary>
  /// Gets the Kind.
  /// </summary>
  public string Kind { get; } = "ClusterIssuer";

  /// <summary>
  /// Gets or sets the metadata.
  /// </summary>
  public required V1ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Gets or sets the spec.
  /// </summary>
  public required CertManagerClusterIssuerSpec Spec { get; set; }
}
