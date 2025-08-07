namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// FilterTags enables filtering for only a subset of tags based on a set of rules.
/// </summary>
public class FluxImagePolicySpecFilterTags
{
  /// <summary>
  /// Pattern is a regular expression to filter the image tags.
  /// </summary>
  public string? Pattern { get; set; }

  /// <summary>
  /// Extract allows a capture group to be extracted from the specified regular expression pattern.
  /// </summary>
  public string? Extract { get; set; }
}