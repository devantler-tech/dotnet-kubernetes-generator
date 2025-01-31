namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Metadata of a HelmRelease.
/// </summary>
public class FluxNamespacedMetadata
{
  /// <summary>
  /// The name of the HelmRelease.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the HelmRelease.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// A map used for setting labels on an object.
  /// </summary>
  public IDictionary<string, string>? Labels { get; set; }
}
