namespace Devantler.KubernetesGenerator.CertManager.Models;

/// <summary>
/// Represents the spec of a Cert Manager ClusterIssuer.
/// </summary>
public class CertManagerClusterIssuerSpec
{
  /// <summary>
  /// Gets or sets the SelfSigned options.
  /// </summary>
  public object? SelfSigned { get; set; }
}
