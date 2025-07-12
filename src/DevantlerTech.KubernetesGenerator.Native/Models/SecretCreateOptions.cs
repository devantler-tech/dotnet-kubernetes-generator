namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Secret creation configuration that can represent any of the three secret types.
/// </summary>
public class SecretCreateOptions
{
  /// <summary>
  /// Generic secret options.
  /// </summary>
  public GenericSecretOptions? Generic { get; set; }

  /// <summary>
  /// Docker registry secret options.
  /// </summary>
  public DockerRegistrySecretOptions? DockerRegistry { get; set; }

  /// <summary>
  /// TLS secret options.
  /// </summary>
  public TlsSecretOptions? Tls { get; set; }

  /// <summary>
  /// Gets the secret type based on which options are set.
  /// </summary>
  public string SecretType =>
    Generic != null ? "generic" :
    DockerRegistry != null ? "docker-registry" :
    Tls != null ? "tls" :
    throw new InvalidOperationException("No secret type specified");

  /// <summary>
  /// Gets the base secret options.
  /// </summary>
  public SecretOptions BaseOptions =>
    Generic != null ? Generic :
    DockerRegistry != null ? DockerRegistry :
    Tls ?? throw new InvalidOperationException("No secret options specified");
}
