namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a TLS secret for use with kubectl create secret tls.
/// </summary>
public class TLSSecret(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the certificate data, either as a file path or the certificate content.
  /// </summary>
  public required string Certificate { get; set; }

  /// <summary>
  /// Gets or sets the private key data, either as a file path or the private key content.
  /// </summary>
  public required string PrivateKey { get; set; }
}
