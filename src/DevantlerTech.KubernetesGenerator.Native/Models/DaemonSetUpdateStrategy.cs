namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the update strategy for a DaemonSet.
/// </summary>
public class DaemonSetUpdateStrategy
{
  /// <summary>
  /// Gets or sets the type of daemon set update.
  /// </summary>
  public DaemonSetUpdateStrategyType? Type { get; set; }

  /// <summary>
  /// Gets or sets the rolling update configuration parameters. Present only if type = "RollingUpdate".
  /// </summary>
  public DaemonSetRollingUpdateStrategy? RollingUpdate { get; set; }
}

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

/// <summary>
/// Represents the rolling update strategy configuration for a DaemonSet.
/// </summary>
public class DaemonSetRollingUpdateStrategy
{
  /// <summary>
  /// Gets or sets the maximum number of DaemonSet pods that can be unavailable during the update.
  /// </summary>
  public string? MaxUnavailable { get; set; }

  /// <summary>
  /// Gets or sets the maximum number of nodes with an existing available DaemonSet pod that can have an updated DaemonSet pod during during an update.
  /// </summary>
  public string? MaxSurge { get; set; }
}