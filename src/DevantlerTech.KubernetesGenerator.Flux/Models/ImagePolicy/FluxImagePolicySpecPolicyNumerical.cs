namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// Numerical gives a lower and upper bound for matching against a numerical value.
/// </summary>
public class FluxImagePolicySpecPolicyNumerical
{
  /// <summary>
  /// Order specifies the sorting order of the tags.
  /// </summary>
  public required FluxImagePolicySpecPolicySortOrder Order { get; set; }
}