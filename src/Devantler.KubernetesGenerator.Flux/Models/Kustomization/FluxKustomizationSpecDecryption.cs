using System.ComponentModel.DataAnnotations;

namespace Devantler.KubernetesGenerator.Flux.Models.Kustomization;

/// <summary>
/// The configuration to decrypt secrets in the Kustomization.
/// </summary>
public class FluxKustomizationSpecDecryption
{
  /// <summary>
  /// The provider to use for decryption.
  /// </summary>

  public required FluxKustomizationSpecDecryptionProvider Provider { get; set; } = FluxKustomizationSpecDecryptionProvider.SOPS;

  /// <summary>
  /// A reference to a Secret containing the private key to use for decryption.
  /// </summary>
  public FluxSecretRef? SecretRef { get; set; }
}
