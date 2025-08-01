using DevantlerTech.KubernetesGenerator.Native.Models.Pod;

namespace DevantlerTech.KubernetesGenerator.Native.Models.DaemonSet;

/// <summary>
/// Represents the specification for a DaemonSet.
/// </summary>
public class NativeDaemonSetSpec
{
  /// <summary>
  /// Gets or sets the label selector for pods. Existing ReplicaSets whose pods are matching this selector will be managed by this DaemonSet.
  /// </summary>
  public required LabelSelector Selector { get; set; }

  /// <summary>
  /// Gets or sets the pod template for this DaemonSet.
  /// </summary>
  public required NativePodTemplateSpec Template { get; set; }

  /// <summary>
  /// Gets or sets the update strategy for this DaemonSet.
  /// </summary>
  public NativeDaemonSetUpdateStrategy? UpdateStrategy { get; set; }

  /// <summary>
  /// Gets or sets the minimum number of seconds for which a newly created DaemonSet pod should be ready without any of its container crashing.
  /// </summary>
  public int? MinReadySeconds { get; set; }

  /// <summary>
  /// Gets or sets the number of old history to retain to allow rollback.
  /// </summary>
  public int? RevisionHistoryLimit { get; set; }
}
