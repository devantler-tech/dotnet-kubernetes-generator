namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// HelmRelease that must be ready before this release can be installed.
/// </summary>
public class FluxDependsOn
{
  /// <summary>
  /// Name of the HelmRelease.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Namespace of the HelmRelease.
  /// </summary>
  public string? Namespace { get; set; } = "";
}
