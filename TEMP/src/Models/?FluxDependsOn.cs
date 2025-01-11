namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// A reference to an object that this object depends on.
/// </summary>
public class FluxDependsOn
{
  /// <summary>
  /// The name of the object that this object depends on.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace of the object that the this object depends on.
  /// </summary>
  public string? Namespace { get; set; }
}

