namespace Devantler.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// A node in the Kind cluster.
/// </summary>
public class KindNode
{
  /// <summary>
  /// The role of the node.
  /// </summary>
  public KindNodeRole Role { get; set; } = KindNodeRole.ControlPlane;

  /// <summary>
  /// The image to use for the node.
  /// </summary>
  public string Image { get; set; } = "kindest/node:v1.32.1";

  /// <summary>
  /// Extra mounts to add to the node.
  /// </summary>
  public IEnumerable<KindNodeExtraMount> ExtraMounts { get; set; } = [];

  /// <summary>
  /// Extra port mappings to add to the node.
  /// </summary>
  public IEnumerable<KindNodeExtraPortMapping> ExtraPortMappings { get; set; } = [];

  /// <summary>
  /// Labels to add to the node.
  /// </summary>
  public Dictionary<string, string> KindNodeLabels { get; } = [];

  /// <summary>
  /// Kubeadm configuration patches to apply to the node.
  /// </summary>
  public string KubeadmConfigPatches { get; set; } = "";
}
