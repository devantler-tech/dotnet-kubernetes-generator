using k8s.Models;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A Flux HelmRelease.
/// </summary>
public class FluxHelmRelease
{
  /// <summary>
  /// API version to retrieve the Kubernetes object from.
  /// </summary>
  public string ApiVersion { get; } = "helm.toolkit.fluxcd.io/v2";

  /// <summary>
  /// Kind of Kubernetes object to retrieve.
  /// </summary>
  public string Kind { get; } = "HelmRelease";

  /// <summary>
  /// Metadata of the HelmRelease.
  /// </summary>
  public required V1ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Spec of the HelmRelease.
  /// </summary>
  public required FluxHelmReleaseSpec Spec { get; set; }
}
