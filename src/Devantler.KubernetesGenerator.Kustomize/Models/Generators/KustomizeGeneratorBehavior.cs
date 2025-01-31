using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.Kustomize.Models.Generators;

/// <summary>
/// The behavior of the generator.
/// </summary>
public enum KustomizeGeneratorBehavior
{
  /// <summary>
  /// Create the ConfigMap if it does not exist.
  /// </summary>
  [EnumMember(Value = "create")]
  Create,
  /// <summary>
  /// Replace the ConfigMap if it exists.
  /// </summary>
  [EnumMember(Value = "replace")]
  Replace,
  /// <summary>
  /// Merge the ConfigMap with the existing one.
  /// </summary>
  [EnumMember(Value = "merge")]
  Merge
}
