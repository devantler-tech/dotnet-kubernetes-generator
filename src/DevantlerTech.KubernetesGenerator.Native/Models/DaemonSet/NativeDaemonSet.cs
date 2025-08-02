using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models.DaemonSet;

/// <summary>
/// Represents a Kubernetes DaemonSet.
/// </summary>
public class NativeDaemonSet
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
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the DaemonSet.
  /// </summary>
  public required NativeDaemonSetSpec Spec { get; init; }
}
