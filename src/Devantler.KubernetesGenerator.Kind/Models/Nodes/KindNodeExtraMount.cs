using System.Text.Json.Serialization;

namespace Devantler.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// An extra mount to add to a Kind node.
/// </summary>
public class KindNodeExtraMount
{
  /// <summary>
  /// The path on the host to mount.
  /// </summary>
  public required string HostPath { get; set; }

  /// <summary>
  /// The path in the container to mount the host path to.
  /// </summary>
  public required string ContainerPath { get; set; }

  /// <summary>
  /// Whether the mount is read-only.
  /// </summary>
  public bool ReadOnly { get; set; }

  /// <summary>
  /// Whether to relabel the mount for SELinux.
  /// </summary>
  [JsonPropertyName("selinuxRelabel")]
  public bool SELinuxRelabel { get; set; }

  /// <summary>
  /// The propagation mode for the mount.
  /// </summary>
  public KindNodeExtraMountPropagation Propagation { get; set; } = KindNodeExtraMountPropagation.None;
}
