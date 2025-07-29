namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the type of DaemonSet update strategy.
/// </summary>
public enum DaemonSetUpdateStrategyType
{
  /// <summary>
  /// Replace the old daemons only when it's killed.
  /// </summary>
  OnDelete,

  /// <summary>
  /// Replace the old daemons by new ones using rolling update i.e replace them on each node one after the other.
  /// </summary>
  RollingUpdate
}