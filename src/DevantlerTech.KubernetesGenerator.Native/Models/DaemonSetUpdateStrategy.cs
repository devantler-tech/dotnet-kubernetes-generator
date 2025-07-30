namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the update strategy for a DaemonSet.
/// </summary>
public class DaemonSetUpdateStrategy
{
  /// <summary>
  /// Gets or sets the type of daemon set update.
  /// </summary>
  public UpdateStrategyType? Type { get; set; }

  /// <summary>
  /// Gets or sets the rolling update configuration parameters. Present only if type = "RollingUpdate".
  /// </summary>
  public DaemonSetRollingUpdateStrategy? RollingUpdate { get; set; }
}
