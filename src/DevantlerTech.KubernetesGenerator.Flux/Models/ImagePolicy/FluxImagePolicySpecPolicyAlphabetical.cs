namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// Alphabetical set of rules to use for alphabetical ordering of the tags.
/// </summary>
public class FluxImagePolicySpecPolicyAlphabetical
{
  /// <summary>
  /// Order specifies the sorting order of the tags.
  /// </summary>
  public required FluxImagePolicySpecPolicySortOrder Order { get; set; }
}