namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Deployment for use with kubectl create deployment.
/// </summary>
public class KubernetesDeployment
{
  /// <summary>
  /// Gets or sets the metadata for the deployment.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Gets or sets the container image to deploy.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the number of replicas for the deployment.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the port for the deployment.
  /// </summary>
  public int? Port { get; set; }

  /// <summary>
  /// Gets or sets additional environment variables for the deployment.
  /// </summary>
  public Dictionary<string, string>? Environment { get; init; }
}