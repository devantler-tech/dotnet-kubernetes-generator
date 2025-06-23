namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// Metadata of a HelmRelease.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="FluxNamespacedMetadata"/>.
/// </remarks>
/// <param name="labels"></param>
public class FluxNamespacedMetadata(IDictionary<string, string>? labels = default)
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
  public IDictionary<string, string>? Labels { get; } = labels;
}
