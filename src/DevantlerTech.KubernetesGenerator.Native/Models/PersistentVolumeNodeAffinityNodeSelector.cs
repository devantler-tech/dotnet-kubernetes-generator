namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a node selector for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeAffinityNodeSelector
{
  /// <summary>
  /// Gets or sets the node selector terms.
  /// </summary>
  public IList<NodeSelectorTerm>? NodeSelectorTerms { get; init; }
}
