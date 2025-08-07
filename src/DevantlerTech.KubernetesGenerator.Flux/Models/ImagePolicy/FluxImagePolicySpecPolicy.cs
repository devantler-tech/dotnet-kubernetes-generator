namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// Policy to use for filtering tags.
/// </summary>
public class FluxImagePolicySpecPolicy
{
  /// <summary>
  /// SemVer gives a semantic version range to check against the tags available.
  /// </summary>
  public FluxImagePolicySpecPolicySemVer? SemVer { get; set; }

  /// <summary>
  /// Numerical gives a lower and upper bound for matching against a numerical value.
  /// </summary>
  public FluxImagePolicySpecPolicyNumerical? Numerical { get; set; }

  /// <summary>
  /// Alphabetical set of rules to use for alphabetical ordering of the tags.
  /// </summary>
  public FluxImagePolicySpecPolicyAlphabetical? Alphabetical { get; set; }
}