namespace Devantler.KubernetesGenerator.Kind.Models.Nodes;

/// <summary>
/// The propagation mode for an extra mount on a Kind node.
/// </summary>
public enum KindNodeExtraMountPropagation
{
  /// <summary>
  /// No propagation mode.
  /// </summary>
  None,
  /// <summary>
  /// Host to container propagation mode.
  /// </summary>
  HostToContainer,

  /// <summary>
  /// Bidirectional propagation mode.
  /// </summary>
  Bidirectional
}
