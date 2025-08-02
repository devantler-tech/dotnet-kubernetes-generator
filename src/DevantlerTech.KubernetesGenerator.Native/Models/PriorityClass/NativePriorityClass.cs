using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models.PriorityClass;

/// <summary>
/// Represents a Kubernetes PriorityClass for use with kubectl create priorityclass commands.
/// </summary>
public class NativePriorityClass
{
  /// <summary>
  /// Gets or sets the API version of this PriorityClass.
  /// </summary>
  public string ApiVersion { get; set; } = "scheduling.k8s.io/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "PriorityClass";

  /// <summary>
  /// Gets or sets the metadata for the priority class.
  /// </summary>
  public required ClusterScopedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the priority value (required). Higher numbers indicate higher priority.
  /// </summary>
  public int Value { get; set; }

  /// <summary>
  /// Gets or sets the description that provides guidelines on when this priority class should be used.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// Gets or sets whether this PriorityClass should be considered as the default priority.
  /// </summary>
  public bool? GlobalDefault { get; set; }

  /// <summary>
  /// Gets or sets the policy for preempting pods with lower priority.
  /// </summary>
  public NativePreemptionPolicy? PreemptionPolicy { get; set; }
}
