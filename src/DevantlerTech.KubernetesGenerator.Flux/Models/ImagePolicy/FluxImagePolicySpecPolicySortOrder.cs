namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// Sorting order for policies.
/// </summary>
public enum FluxImagePolicySpecPolicySortOrder
{
  /// <summary>
  /// Ascending order - select the last/highest.
  /// </summary>
  Asc,

  /// <summary>
  /// Descending order - select the first/lowest.
  /// </summary>
  Desc
}