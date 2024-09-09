using Devantler.KubernetesGenerator.Kustomize.Models.Patches;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Devantler.KubernetesGenerator.Flux.Models.Patches;

/// <summary>
/// A patch to apply to some of the objects resources.
/// </summary>
public class FluxPatch
{
  /// <summary>
  /// Inline patch content.
  /// </summary>
  [YamlMember(ScalarStyle = ScalarStyle.Literal)]
  public string? Patch { get; set; }

  /// <summary>
  /// The target resource(s) to apply the patch to.
  /// </summary>
  public KustomizeTarget? Target { get; set; }
}
