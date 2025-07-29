namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the update strategy type for a StatefulSet.
/// </summary>
public enum StatefulSetUpdateStrategyType
{
  /// <summary>
  /// Recreate strategy.
  /// </summary>
  OnDelete,

  /// <summary>
  /// Rolling update strategy (default).
  /// </summary>
  RollingUpdate
}