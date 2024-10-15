using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.KSail.Models.Init;

/// <summary>
/// The template to use for initializing a KSail cluster.
/// </summary>
public enum KSailInitTemplate
{
  /// <summary>
  /// The default template for initializing a KSail cluster with Flux and a minimal Kustomize setup.
  /// </summary>
  [EnumMember(Value = "flux-default")]
  FluxDefault,

  /// <summary>
  /// An advanced template for initializing a KSail cluster with Flux and a scalable Kustomize setup.
  /// </summary>
  [EnumMember(Value = "flux-advanced")]
  FluxAdvanced,
}
