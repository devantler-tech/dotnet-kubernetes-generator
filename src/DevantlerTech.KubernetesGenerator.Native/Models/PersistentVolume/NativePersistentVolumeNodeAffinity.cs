namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Represents node affinity for a persistent volume.
/// </summary>
public class NativePersistentVolumeNodeAffinity
{
  /// <summary>
  /// Gets or sets the required node selector.
  /// </summary>
  public NativePersistentVolumeNodeAffinityNodeSelector? Required { get; set; }
}
