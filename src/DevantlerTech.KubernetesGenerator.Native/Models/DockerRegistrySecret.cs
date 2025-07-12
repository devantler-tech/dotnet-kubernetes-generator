using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Docker registry secret for use with kubectl create secret docker-registry.
/// </summary>
public class DockerRegistrySecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry server URL.
  /// </summary>
  public string? DockerServer { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry username.
  /// </summary>
  public string? DockerUsername { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry password.
  /// </summary>
  public string? DockerPassword { get; set; }

  /// <summary>
  /// Gets or sets the Docker registry email.
  /// </summary>
  public string? DockerEmail { get; set; }
}
