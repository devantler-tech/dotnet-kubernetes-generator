namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Flux source reference.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFluxSourceRef<T> where T : Enum
{
  /// <summary>
  /// The type of the source reference.
  /// </summary>
  T Kind { get; set; }

  /// <summary>
  /// The name of the source reference.
  /// </summary>
  string Name { get; set; }

  /// <summary>
  /// The namespace of the source reference.
  /// </summary>
  string? Namespace { get; set; }
}
