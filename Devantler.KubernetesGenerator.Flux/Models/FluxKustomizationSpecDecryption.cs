namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The configuration to decrypt secrets in the Kustomization.
/// </summary>
public class FluxKustomizationSpecDecryption
{
  /// <summary>
  /// The provider to use for decryption.
  /// </summary>
  public required FluxKustomizationSpecDecryptionProvider Provider { get; set; }

  /// <summary>
  /// A reference to a Secret containing the private key to use for decryption.
  /// </summary>
  public required FluxSecretRef? SecretRef { get; set; }
}
