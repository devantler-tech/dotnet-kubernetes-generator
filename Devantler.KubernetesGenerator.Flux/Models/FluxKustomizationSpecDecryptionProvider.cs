using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The provider to use for decryption.
/// </summary>
public enum FluxKustomizationSpecDecryptionProvider
{
  /// <summary>
  /// The provider to use for decryption is sops.
  /// </summary>
  [EnumMember(Value = "sops")]
  SOPS
}
