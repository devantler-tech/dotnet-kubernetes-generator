namespace DevantlerTech.KubernetesGenerator.Kubectl.Models;

/// <summary>
/// Model for kubectl TLS secret generation.
/// </summary>
public class KubectlTlsSecret : KubectlSecretBase
{
  /// <summary>
  /// Path to PEM encoded public key certificate.
  /// </summary>
  public string CertPath { get; set; } = string.Empty;

  /// <summary>
  /// Path to private key associated with given certificate.
  /// </summary>
  public string KeyPath { get; set; } = string.Empty;
}