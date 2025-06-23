namespace DevantlerTech.KubernetesGenerator.CertManager.Models.IssuerRef;

/// <summary>
/// Represents the issuer reference of a Cert Manager resource.
/// </summary>
public class CertManagerIssuerRef
{
  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the kind.
  /// </summary>
  public required string Kind { get; set; }
}
