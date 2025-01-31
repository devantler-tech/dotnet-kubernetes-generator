namespace Devantler.KubernetesGenerator.Kustomize.Models.Patches;

/// <summary>
/// Options for the patch.
/// </summary>
public class KustomizePatchOptions
{
  /// <summary>
  /// Allows overridding the name of the target resource.
  /// </summary>
  public bool AllowNameChange { get; set; }

  /// <summary>
  /// Allows overridding the kind of the target resource.
  /// </summary>
  public bool AllowKindChange { get; set; }
}
