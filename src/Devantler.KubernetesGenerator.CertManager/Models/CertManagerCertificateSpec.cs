using Devantler.KubernetesGenerator.CertManager.Models.IssuerRef;

namespace Devantler.KubernetesGenerator.CertManager.Models;

/// <summary>
/// Represents the spec of a Cert Manager Certificate.
/// </summary>
public class CertManagerCertificateSpec
{
  /// <summary>
  /// Gets or sets the secret name.
  /// </summary>
  public required string SecretName { get; set; }

  /// <summary>
  /// Gets or sets the DNS names.
  /// </summary>
  public required IEnumerable<string> DnsNames { get; set; }

  /// <summary>
  /// Gets or sets the issuer reference.
  /// </summary>
  public required CertManagerIssuerRef IssuerRef { get; set; }
}
