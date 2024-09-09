using k8s.Models;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A pipeline for fetching, decrypting, building, validating and applying Kustomize overlays or plain Kubernetes manifests.
/// </summary>
public class FluxKustomization
{
  /// <summary>
  /// The API version of the Flux Kustomization object.
  /// </summary>
  public string ApiVersion { get; } = "kustomize.toolkit.fluxcd.io/v1";

  /// <summary>
  /// The kind of the Flux Kustomization object.
  /// </summary>
  public string Kind { get; } = "Kustomization";

  /// <summary>
  /// The metadata of the Flux Kustomization object.
  /// </summary>
  public required V1ObjectMeta Metadata { get; set; }

  /// <summary>
  /// The spec of the Flux Kustomization object.
  /// </summary>
  public required FluxKustomizationSpec Spec { get; set; }
}
