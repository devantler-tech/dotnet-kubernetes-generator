using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Devantler.KubernetesGenerator.Kustomize.Models.Patches;

/// <summary>
/// A kustomize feature to apply customizations to existing resources. (strategic-merge-patch)
/// </summary>
public class KustomizePatch
{
  /// <summary>
  /// The path to the patch file.
  /// </summary>
  public string? Path { get; set; }

  /// <summary>
  /// Inline patch content.
  /// </summary>
  [YamlMember(ScalarStyle = ScalarStyle.Literal)]
  public string? Patch { get; set; }

  /// <summary>
  /// The target resource(s) to apply the patch to.
  /// </summary>
  public KustomizeTarget? Target { get; set; }

  /// <summary>
  /// Options for the patch.
  /// </summary>
  public KustomizePatchOptions? Options { get; set; }
}
