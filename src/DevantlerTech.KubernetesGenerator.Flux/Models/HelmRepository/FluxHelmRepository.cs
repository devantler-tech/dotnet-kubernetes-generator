namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRepository;

/// <summary>
/// A Flux HelmRepository.
/// </summary>
public class FluxHelmRepository
{
  /// <summary>
  /// API version to retrieve the Kubernetes object from.
  /// </summary>
  public string ApiVersion { get; } = "source.toolkit.fluxcd.io/v1";

  /// <summary>
  /// Kind of Kubernetes object to retrieve.
  /// </summary>
  public string Kind { get; } = "HelmRepository";

  /// <summary>
  /// Metadata of the HelmRepository.
  /// </summary>
  public required FluxNamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Spec of the HelmRepository.
  /// </summary>
  public required FluxHelmRepositorySpec Spec { get; set; }
}
