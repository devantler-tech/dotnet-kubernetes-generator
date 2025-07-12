using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a TLS secret for use with kubectl create secret tls.
/// </summary>
public class TLSSecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the path to the certificate file.
  /// </summary>
  public string? CertificatePath { get; set; }

  /// <summary>
  /// Gets or sets the path to the private key file.
  /// </summary>
  public string? PrivateKeyPath { get; set; }

  /// <summary>
  /// Gets or sets the certificate content as a string.
  /// </summary>
  public string? CertificateContent { get; set; }

  /// <summary>
  /// Gets or sets the private key content as a string.
  /// </summary>
  public string? PrivateKeyContent { get; set; }
}