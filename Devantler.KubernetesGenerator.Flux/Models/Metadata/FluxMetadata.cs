namespace Devantler.KubernetesGenerator.Flux.Models.Metadata;

/// <summary>
/// Metadata that should be applied to all the objects resources.
/// </summary>
public class FluxMetadata
{
  /// <summary>
  /// A map used for setting labels on an object.
  /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
  public IDictionary<string, string>? Labels { get; set; }

  /// <summary>
  /// A map used for setting annotations on an object.
  /// </summary>
  public IDictionary<string, string>? Annotations { get; set; }
#pragma warning restore CA2227
}
