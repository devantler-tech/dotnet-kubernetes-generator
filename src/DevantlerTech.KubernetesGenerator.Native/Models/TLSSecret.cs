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
  /// Gets or sets the certificate data, either as a file path or the certificate content.
  /// </summary>
  public string? Certificate { get; set; }

  /// <summary>
  /// Gets or sets the private key data, either as a file path or the private key content.
  /// </summary>
  public string? PrivateKey { get; set; }
}
