namespace Devantler.KubernetesGenerator.Kustomize.Models.Generators;

/// <summary>
/// A kustomize feature to generate ConfigMap resources from files or literals.
/// </summary>
public class KustomizeConfigMapGenerator
{
  /// <summary>
  /// The name of the ConfigMap to generate.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The behavior of the generator. (e.g. `create`, `merge`, `replace`)
  /// </summary>
  public KustomizeGeneratorBehavior? Behavior { get; set; }

  /// <summary>
  /// Environment variables to populate the ConfigMap's data.
  /// </summary>
  public IEnumerable<string>? Envs { get; set; }

  /// <summary>
  /// Files containing literals to populate the ConfigMap's data.
  /// </summary>
  public IEnumerable<string>? Files { get; set; }

  /// <summary>
  /// Literals to populate the ConfigMap's data.
  /// </summary>
  public IEnumerable<string>? Literals { get; set; }

  /// <summary>
  /// Options for the generator.
  /// </summary>
  public KustomizeGeneratorOptions? Options { get; set; }
}
