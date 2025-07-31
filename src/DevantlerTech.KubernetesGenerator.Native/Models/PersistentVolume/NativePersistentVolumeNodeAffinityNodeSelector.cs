namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents a node selector for persistent volume node affinity.
/// </summary>
public class NativePersistentVolumeNodeAffinityNodeSelector
{
  /// <summary>
  /// Gets or sets the node selector terms.
  /// </summary>
  public IList<NativeNodeSelectorTerm>? NodeSelectorTerms { get; init; }
}
