namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the preemption policy for a PriorityClass.
/// </summary>
public enum PreemptionPolicy
{
  /// <summary>
  /// Pods with this priority class can preempt lower priority pods.
  /// </summary>
  PreemptLowerPriority,

  /// <summary>
  /// Pods with this priority class cannot preempt any other pods.
  /// </summary>
  Never
}
