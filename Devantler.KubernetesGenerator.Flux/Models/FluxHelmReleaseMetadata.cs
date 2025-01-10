namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Metadata of a HelmRelease.
/// </summary>
public class FluxHelmReleaseMetadata
{
  /// <summary>
  /// The name of the HelmRelease.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the HelmRelease.
  /// </summary>
  public string? Namespace { get; set; }
}
