namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// An image to replace if encountered in the objects resources.
/// </summary>
public class FluxImage
{
  /// <summary>
  /// The name of the image to replace.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The new image to replace the old image with.
  /// </summary>
  public required string NewName { get; set; }

  /// <summary>
  /// The new tag to replace the old tag with.
  /// </summary>
  public string? NewTag { get; set; }

  /// <summary>
  /// The digest to use for the new image.
  /// </summary>
  public string? Digest { get; set; }
}
