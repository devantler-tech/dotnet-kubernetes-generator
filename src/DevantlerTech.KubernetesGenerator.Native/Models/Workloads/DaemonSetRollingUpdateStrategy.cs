namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

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
  /// Gets or sets the maximum number of nodes with an existing available DaemonSet pod that can have an updated DaemonSet pod during an update.
  /// </summary>
  public string? MaxSurge { get; set; }
}
