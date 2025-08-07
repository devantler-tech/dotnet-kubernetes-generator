namespace DevantlerTech.KubernetesGenerator.Flux.Models.ImagePolicy;

/// <summary>
/// SemVer gives a semantic version range to check against the tags available.
/// </summary>
public class FluxImagePolicySpecPolicySemVer
{
  /// <summary>
  /// Range gives a semver range for the image tag.
  /// </summary>
  public required string Range { get; set; }
}