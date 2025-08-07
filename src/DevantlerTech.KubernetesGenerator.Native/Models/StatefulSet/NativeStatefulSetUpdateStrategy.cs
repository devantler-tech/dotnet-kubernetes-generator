using DevantlerTech.KubernetesGenerator.Core.Models;
namespace DevantlerTech.KubernetesGenerator.Native.Models.StatefulSet;

/// <summary>
/// Represents the update strategy for a StatefulSet.
/// </summary>
public class NativeStatefulSetUpdateStrategy
{
  /// <summary>
  /// Gets or sets the type of update strategy.
  /// </summary>
  public UpdateStrategyType? Type { get; set; }

  /// <summary>
  /// Gets or sets the rolling update strategy configuration. Only used when Type is RollingUpdate.
  /// </summary>
  public NativeStatefulSetRollingUpdateStrategy? RollingUpdate { get; set; }
}
