namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// ImageRepositoryRef specifies the name and optionally the namespace of an ImageRepository resource.
/// </summary>
public class FluxImagePolicySpecImageRepositoryRef
{
  /// <summary>
  /// Name of the ImageRepository.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Namespace of the ImageRepository. If not specified, the namespace of the ImagePolicy is used.
  /// </summary>
  public string? Namespace { get; set; }
}