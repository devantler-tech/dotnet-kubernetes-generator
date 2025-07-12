using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Options for creating a docker-registry secret.
/// </summary>
public class DockerRegistrySecretOptions : SecretOptions
{
  /// <summary>
  /// Docker server URL.
  /// </summary>
  public string? DockerServer { get; set; } = "https://index.docker.io/v1/";

  /// <summary>
  /// Docker username.
  /// </summary>
  public string? DockerUsername { get; set; }

  /// <summary>
  /// Docker password.
  /// </summary>
  public string? DockerPassword { get; set; }

  /// <summary>
  /// Docker email.
  /// </summary>
  public string? DockerEmail { get; set; }

  /// <summary>
  /// Files to create the secret from (alternative to docker credentials).
  /// </summary>
  public Collection<string> FromFiles { get; } = [];
}
