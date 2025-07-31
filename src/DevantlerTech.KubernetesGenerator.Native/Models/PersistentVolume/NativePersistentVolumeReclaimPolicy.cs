namespace DevantlerTech.KubernetesGenerator.Native.Models.PersistentVolume;

/// <summary>
/// Enum for supported PersistentVolume reclaim policies.
/// </summary>
public enum NativePersistentVolumeReclaimPolicy
{
  /// <summary>
  /// Retain the persistent volume for manual cleanup.
  /// </summary>
  Retain,

  /// <summary>
  /// Delete the persistent volume and its storage.
  /// </summary>
  Delete,

  /// <summary>
  /// Recycle the persistent volume for reuse (deprecated).
  /// </summary>
  Recycle
}
