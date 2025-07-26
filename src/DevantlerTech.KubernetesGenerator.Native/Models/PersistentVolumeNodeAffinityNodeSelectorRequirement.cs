namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a node selector requirement for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeAffinityNodeSelectorRequirement
{
  /// <summary>
  /// Gets or sets the key.
  /// </summary>
  public required string Key { get; set; }

  /// <summary>
  /// Gets or sets the operator.
  /// </summary>
  public required PersistentVolumeNodeAffinityNodeSelectorRequirementOperator Operator { get; set; }

  /// <summary>
  /// Gets or sets the values.
  /// </summary>
  public IList<string>? Values { get; init; }
}