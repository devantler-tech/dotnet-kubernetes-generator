using System.Diagnostics.CodeAnalysis;

namespace DevantlerTech.KubernetesGenerator.Native.Models.Deployment;

/// <summary>
/// Represents a Kubernetes Deployment for use with kubectl create deployment.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a specific Kubernetes resource model")]
public class NativeDeployment
{
  /// <summary>
  /// Gets or sets the API version of this Deployment.
  /// </summary>
  public string ApiVersion { get; set; } = "apps/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "Deployment";

  /// <summary>
  /// Gets or sets the metadata for the deployment.
  /// </summary>
  public required NativeMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the deployment.
  /// </summary>
  public required NativeDeploymentSpec Spec { get; init; }
}