using System.ComponentModel;

namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// The provider to use for decryption.
/// </summary>
public enum FluxKustomizationSpecDecryptionProvider
{
  /// <summary>
  /// The provider to use for decryption is sops.
  /// </summary>
  [Description("sops")]
  SOPS
}
