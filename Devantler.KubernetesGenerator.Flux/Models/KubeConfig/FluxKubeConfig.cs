using Devantler.KubernetesGenerator.Flux.Models.SecretRef;

namespace Devantler.KubernetesGenerator.Flux.Models.KubeConfig;

/// <summary>
/// The kubeconfig to use for the reconciliation of the object.
/// </summary>
public class FluxKubeConfig
{
  /// <summary>
  /// A reference to a Secret containing a kubeconfig to use for the reconciliation of the object.
  /// </summary>
  public required FluxSecretRef? SecretRef { get; set; }
}
