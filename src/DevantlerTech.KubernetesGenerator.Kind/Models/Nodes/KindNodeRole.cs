using System.Runtime.Serialization;

namespace DevantlerTech.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// Supported roles for a Kind node.
/// </summary>
public enum KindNodeRole
{
  /// <summary>
  /// The node is a control plane node.
  /// </summary>
  [EnumMember(Value = "control-plane")]
  ControlPlane,
  /// <summary>
  /// The node is a worker node.
  /// </summary>
  Worker
}
