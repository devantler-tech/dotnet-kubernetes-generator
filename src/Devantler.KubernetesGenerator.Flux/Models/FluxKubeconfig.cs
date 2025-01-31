namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// Options for setting remote kubeconfig.
/// </summary>
public class FluxKubeconfig
{
  /// <summary>
  /// Reference to the Kubernetes Secret that contains a key with the kubeconfig file for connecting to a remote cluster.
  /// </summary>
  public required FluxSecretRef? SecretRef { get; set; }
}
