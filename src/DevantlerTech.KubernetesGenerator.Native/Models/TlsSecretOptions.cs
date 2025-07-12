namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Options for creating a TLS secret.
/// </summary>
public class TlsSecretOptions : SecretOptions
{
  /// <summary>
  /// Path to the certificate file.
  /// </summary>
  public required string CertPath { get; set; }

  /// <summary>
  /// Path to the private key file.
  /// </summary>
  public required string KeyPath { get; set; }
}
