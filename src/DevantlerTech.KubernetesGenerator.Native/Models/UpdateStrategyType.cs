namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the update strategy type for Kubernetes workloads.
/// </summary>
public enum UpdateStrategyType
{
  /// <summary>
  /// Replace the old pods only when they are killed (manual deletion).
  /// </summary>
  OnDelete,

  /// <summary>
  /// Replace the old pods using rolling update strategy.
  /// </summary>
  RollingUpdate
}