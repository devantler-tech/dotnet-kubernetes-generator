using Devantler.KubernetesGenerator.Kustomize.Models.Generators;
using Devantler.KubernetesGenerator.Kustomize.Models.Patches;

namespace Devantler.KubernetesGenerator.Kustomize.Models;

/// <summary>
/// A Kustomize Kustomization.
/// </summary>
public class KustomizeKustomization
{
  /// <summary>
  /// API version to retrieve the Kubernetes object from.
  /// </summary>
  public string ApiVersion { get; } = "kustomize.config.k8s.io/v1beta1";

  /// <summary>
  /// Kind of Kubernetes object to retrieve.
  /// </summary>
  public string Kind { get; } = "Kustomization";

  /// <summary>
  /// Resources to include in the component.
  /// </summary>
  public IEnumerable<string>? Resources { get; set; }

  /// <summary>
  /// A kustomize feature to generate Secret resources from files or literals.
  /// </summary>
  public IEnumerable<KustomizeSecretGenerator>? SecretGenerator { get; set; }

  /// <summary>
  /// A kustomize feature to generate ConfigMap resources from files or literals.
  /// </summary>
  public IEnumerable<KustomizeConfigMapGenerator>? ConfigMapGenerator { get; set; }

  /// <summary>
  /// A kustomize feature to apply customizations to existing resources.
  /// </summary>
  public IEnumerable<KustomizePatch>? Patches { get; set; }

  /// <summary>
  /// A kustomize feature to apply components to the kustomization.
  /// </summary>
  public IEnumerable<string>? Components { get; set; }

}
