namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// Resources for which the controller will perform health checks.
/// </summary>
public class FluxKustomizationSpecHealthCheck
{
  /// <summary>
  /// The kind of the health check resource.
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// The name of the health check resource.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the health check resource.
  /// </summary>
  public required string Namespace { get; set; }
}
