using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents metadata for a cluster role.
/// </summary>
public class ClusterRoleMetadata
{
  /// <summary>
  /// Gets or sets the name of the cluster role.
  /// </summary>
  [YamlMember(Alias = "name")]
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the labels for this cluster role.
  /// </summary>
  [YamlMember(Alias = "labels")]
  public IDictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Gets or sets the annotations for this cluster role.
  /// </summary>
  [YamlMember(Alias = "annotations")]
  public IDictionary<string, string>? Annotations { get; init; }
}