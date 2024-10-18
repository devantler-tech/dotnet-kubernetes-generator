namespace Devantler.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// Supported roles for a Kind node.
/// </summary>
public enum KindNodeRole
{
  /// <summary>
  /// The node is a control plane node.
  /// </summary>
  ControlPlane,
  /// <summary>
  /// The node is a worker node.
  /// </summary>
  Worker
}
