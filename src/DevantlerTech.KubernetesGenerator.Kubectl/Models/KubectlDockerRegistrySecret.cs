namespace DevantlerTech.KubernetesGenerator.Kubectl.Models;

/// <summary>
/// Model for kubectl docker-registry secret generation.
/// </summary>
public class KubectlDockerRegistrySecret : KubectlSecretBase
{
  /// <summary>
  /// Docker registry server (defaults to https://index.docker.io/v1/).
  /// </summary>
  public string DockerServer { get; set; } = "https://index.docker.io/v1/";

  /// <summary>
  /// Username for Docker registry authentication.
  /// </summary>
  public string DockerUsername { get; set; } = string.Empty;

  /// <summary>
  /// Password for Docker registry authentication.
  /// </summary>
  public string DockerPassword { get; set; } = string.Empty;

  /// <summary>
  /// Email for Docker registry (optional).
  /// </summary>
  public string? DockerEmail { get; set; }

  /// <summary>
  /// Path to existing Docker config file instead of using credentials.
  /// </summary>
  public string? FromFile { get; set; }
}