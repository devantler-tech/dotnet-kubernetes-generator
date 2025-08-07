namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// Spec of a Flux ImagePolicy.
/// </summary>
public class FluxImagePolicySpec
{
  /// <summary>
  /// ImageRepositoryRef refers to the ImageRepository that should be evaluated.
  /// </summary>
  public required FluxImagePolicySpecImageRepositoryRef ImageRepositoryRef { get; set; }

  /// <summary>
  /// Policy to use for filtering tags.
  /// </summary>
  public required FluxImagePolicySpecPolicy Policy { get; set; }

  /// <summary>
  /// FilterTags enables filtering for only a subset of tags based on a set of rules.
  /// </summary>
  public FluxImagePolicySpecFilterTags? FilterTags { get; set; }
}