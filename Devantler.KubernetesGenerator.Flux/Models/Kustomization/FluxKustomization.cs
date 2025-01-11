namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// A pipeline for fetching, decrypting, building, validating and applying Kustomize overlays or plain Kubernetes manifests.
/// </summary>
public class FluxKustomization
{
  /// <summary>
  /// The metadata of the Flux Kustomization object.
  /// </summary>
  public required FluxNamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// The spec of the Flux Kustomization object.
  /// </summary>
  public FluxKustomizationSpec? Spec { get; set; }
}
