namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes DaemonSet.
/// </summary>
public class DaemonSet
{
  /// <summary>
  /// Gets or sets the API version of this DaemonSet.
  /// </summary>
  public string ApiVersion { get; set; } = "apps/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "DaemonSet";

  /// <summary>
  /// Gets or sets the metadata for the DaemonSet.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the DaemonSet.
  /// </summary>
  public required DaemonSetSpec Spec { get; init; }
}