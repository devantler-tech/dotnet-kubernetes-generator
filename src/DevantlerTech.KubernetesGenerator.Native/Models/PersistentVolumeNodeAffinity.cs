namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents node affinity for a persistent volume.
/// </summary>
public class PersistentVolumeNodeAffinity
{
  /// <summary>
  /// Gets or sets the required node selector.
  /// </summary>
  public PersistentVolumeNodeAffinityNodeSelector? Required { get; set; }
}
