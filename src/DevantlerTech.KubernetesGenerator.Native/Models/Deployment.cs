using System.Collections.ObjectModel;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Deployment for use with kubectl create deployment.
/// </summary>
public class KubernetesDeployment
{
  /// <summary>
  /// Gets or sets the metadata for the deployment.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets the container images for the deployment.
  /// At least one image is required.
  /// </summary>
  public required Collection<string> Images { get; init; }

  /// <summary>
  /// Gets or sets the number of replicas for the deployment.
  /// Defaults to 1 if not specified.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the container port for the deployment.
  /// Only the first container's first port is supported by kubectl create deployment.
  /// </summary>
  public int? Port { get; set; }
}