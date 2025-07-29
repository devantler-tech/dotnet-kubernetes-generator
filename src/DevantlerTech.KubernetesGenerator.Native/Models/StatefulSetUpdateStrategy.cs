namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the update strategy for a StatefulSet.
/// </summary>
public class StatefulSetUpdateStrategy
{
  /// <summary>
  /// Gets or sets the type of update strategy.
  /// </summary>
  public StatefulSetUpdateStrategyType? Type { get; set; }

  /// <summary>
  /// Gets or sets the rolling update strategy configuration. Only used when Type is RollingUpdate.
  /// </summary>
  public StatefulSetRollingUpdateStrategy? RollingUpdate { get; set; }
}