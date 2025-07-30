namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Docker registry secret for use with kubectl create secret docker-registry.
/// </summary>
public class DockerRegistrySecret(string name) : BaseSecret(name)
{

  /// <summary>
  /// Gets or sets the Docker registry server URL.
  /// </summary>
  public string? DockerServer { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry username.
  /// </summary>
  public required string DockerUsername { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry password.
  /// </summary>
  public required string DockerPassword { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry email.
  /// </summary>
  public required string DockerEmail { get; set; }
}
