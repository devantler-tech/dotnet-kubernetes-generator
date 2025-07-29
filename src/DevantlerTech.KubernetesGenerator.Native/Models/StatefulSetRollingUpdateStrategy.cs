namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the rolling update strategy for a StatefulSet.
/// </summary>
public class StatefulSetRollingUpdateStrategy
{
  /// <summary>
  /// Gets or sets the maximum number of pods that can be updated at one time.
  /// </summary>
  public int? MaxUnavailable { get; set; }

  /// <summary>
  /// Gets or sets the partition for the rolling update.
  /// </summary>
  public int? Partition { get; set; }
}