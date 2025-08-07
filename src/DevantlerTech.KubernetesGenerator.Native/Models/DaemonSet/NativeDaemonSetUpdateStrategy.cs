using DevantlerTech.KubernetesGenerator.Core.Models;
namespace DevantlerTech.KubernetesGenerator.Native.Models.DaemonSet;

/// <summary>
/// Represents the update strategy for a DaemonSet.
/// </summary>
public class NativeDaemonSetUpdateStrategy
{
  /// <summary>
  /// Gets or sets the type of daemon set update.
  /// </summary>
  public UpdateStrategyType? Type { get; set; }

  /// <summary>
  /// Gets or sets the rolling update configuration parameters. Present only if type = "RollingUpdate".
  /// </summary>
  public NativeDaemonSetRollingUpdateStrategy? RollingUpdate { get; set; }
}
