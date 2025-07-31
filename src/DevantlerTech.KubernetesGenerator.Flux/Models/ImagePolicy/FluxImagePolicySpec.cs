namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// The spec of a Flux ImagePolicy.
/// </summary>
public class FluxImagePolicySpec
{
  /// <summary>
  /// The name of an image repository object.
  /// </summary>
  public required string ImageRef { get; set; }

  /// <summary>
  /// The sorting strategy for the image policy.
  /// </summary>
  public FluxImagePolicySortBy SortBy { get; set; } = FluxImagePolicySortBy.Semver;

  /// <summary>
  /// A semver range to apply to tags when using semver sorting.
  /// </summary>
  public string? SemverRange { get; set; }

  /// <summary>
  /// The sort order when using numeric or alphabetical sorting.
  /// </summary>
  public FluxImagePolicySortOrder? SortOrder { get; set; }

  /// <summary>
  /// Regular expression pattern used to filter the image tags.
  /// </summary>
  public string? FilterRegex { get; set; }

  /// <summary>
  /// Replacement pattern (using capture groups from FilterRegex) to use for sorting.
  /// </summary>
  public string? FilterExtract { get; set; }

  /// <summary>
  /// The interval at which to check for new image digests when the policy is set to 'Always'.
  /// </summary>
  public TimeSpan? Interval { get; set; }

  /// <summary>
  /// The digest reflection policy to use when observing latest image tags.
  /// </summary>
  public FluxImagePolicyReflectDigest? ReflectDigest { get; set; }
}