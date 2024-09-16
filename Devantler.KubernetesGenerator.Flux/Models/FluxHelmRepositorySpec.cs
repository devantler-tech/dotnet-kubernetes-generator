namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Spec of a Flux HelmRepository.
/// </summary>
public class FluxHelmRepositorySpec
{
  /// <summary>
  /// Interval at which to check the Helm repository for updates.
  /// </summary>
  public string? Interval { get; set; }
  /// <summary>
  /// URL of the Helm repository.
  /// </summary>
  public required Uri Url { get; set; }

  /// <summary>
  /// Type of the Helm repository.
  /// </summary>
  public FluxHelmRepositorySpecType? Type { get; set; }
}
