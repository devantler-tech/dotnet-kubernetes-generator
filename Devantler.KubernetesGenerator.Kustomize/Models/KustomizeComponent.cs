namespace Devantler.KubernetesGenerator.Kustomize.Models;

/// <summary>
/// A Kustomize component.
/// </summary>
public class KustomizeComponent
{
  /// <summary>
  /// API version to retrieve the Kubernetes object from.
  /// </summary>
  public string ApiVersion { get; } = "kustomize.config.k8s.io/v1alpha1";
  /// <summary>
  /// Kind of Kubernetes object to retrieve.
  /// </summary>
  public string Kind { get; } = "Component";

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
  /// A kustomize feature to apply customizations to existing resources. (strategic-merge-patch)
  /// </summary>
  public IEnumerable<KustomizePatch>? Patches { get; set; }

  /// <summary>
  /// A kustomize feature to apply customizations to existing resources. (json-6902)
  /// </summary>
  public IEnumerable<KustomizePatchJson6902>? PatchesJson6902 { get; set; }
}
