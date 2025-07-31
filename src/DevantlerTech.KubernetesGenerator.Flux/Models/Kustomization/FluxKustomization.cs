using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// A pipeline for fetching, decrypting, building, validating and applying Kustomize overlays or plain Kubernetes manifests.
/// </summary>
public class FluxKustomization
{
  /// <summary>
  /// The API version of the Flux Kustomization object.
  /// </summary>
  public string ApiVersion { get; set; } = "kustomize.toolkit.fluxcd.io/v1";

  /// <summary>
  /// The kind of the Flux Kustomization object.
  /// </summary>
  public string Kind { get; set; } = "Kustomization";

  /// <summary>
  /// The metadata of the Flux Kustomization object.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// The spec of the Flux Kustomization object.
  /// </summary>
  public FluxKustomizationSpec? Spec { get; set; } = new FluxKustomizationSpec
  {
    SourceRef = new FluxKustomizationSpecSourceRef
    {
      Kind = FluxKustomizationSpecSourceRefKind.OCIRepository,
      Name = "flux-system"
    }
  };
}
