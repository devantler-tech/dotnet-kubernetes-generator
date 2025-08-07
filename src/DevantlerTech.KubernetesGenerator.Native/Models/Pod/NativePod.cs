using DevantlerTech.KubernetesGenerator.Core.Models;
namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents a Pod for use with kubectl run.
/// </summary>
public class NativePod
{
  /// <summary>
  /// Gets or sets the API version of this Pod.
  /// </summary>
  public string ApiVersion { get; set; } = "v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "Pod";

  /// <summary>
  /// Gets or sets the metadata for the pod.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the pod.
  /// </summary>
  public required NativePodSpec Spec { get; init; }
}
