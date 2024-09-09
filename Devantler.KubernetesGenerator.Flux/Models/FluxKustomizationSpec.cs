using Devantler.KubernetesGenerator.Flux.Models.Dependencies;
using Devantler.KubernetesGenerator.Flux.Models.Images;
using Devantler.KubernetesGenerator.Flux.Models.KubeConfig;
using Devantler.KubernetesGenerator.Flux.Models.Metadata;
using Devantler.KubernetesGenerator.Flux.Models.Patches;

namespace Devantler.KubernetesGenerator.Flux.Models;

/// <summary>
/// The spec of the Flux Kustomization object.
/// </summary>
public class FluxKustomizationSpec
{
  /// <summary>
  /// The interval at which to reconcile the Kustomization object.
  /// </summary>
  public required string Interval { get; set; }

  /// <summary>
  /// The retry interval for the reconciliation of the Kustomization object.
  /// </summary>
  public string? RetryInterval { get; set; }

  /// <summary>
  /// The timeout before the reconciliation of the Kustomization object is considered failed.
  /// </summary>
  public string? Timeout { get; set; }

  /// <summary>
  /// Suspend the reconciliation of the Kustomization.
  /// </summary>
  public bool? Suspend { get; set; }

  /// <summary>
  /// The namespace the Kustomization object should be reconciled in.
  /// </summary>
  public string? TargetNamespace { get; set; }

  /// <summary>
  /// A list of other Kustomizations that the Kustomization object depends on.
  /// </summary>
  public IEnumerable<FluxDependsOn>? DependsOn { get; set; }

  /// <summary>
  /// The source reference that the Kustomization object should be reconciled with.
  /// </summary>
  public required FluxKustomizationSpecSourceRef SourceRef { get; set; }

  /// <summary>
  /// The path to the Kustomization object.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Enable or disable garbage collection for a Kustomization.
  /// </summary>
  public bool? Prune { get; set; }

  /// <summary>
  /// Perform health checks for all reconciled resources as part of the Kustomization.
  /// </summary>
  public bool? Wait { get; set; }

  /// <summary>
  /// Resources for which the controller will perform health checks.
  /// </summary>
  public IEnumerable<FluxKustomizationSpecHealthCheck>? HealthChecks { get; set; }

  /// <summary>
  /// The ServiceAccount to be impersonated while reconciling the Kustomization.
  /// </summary>
  public string? ServiceAccountName { get; set; }

  /// <summary>
  /// Any metadata that should be applied to all the Kustomization's resources.
  /// </summary>
  public IEnumerable<FluxMetadata>? CommonMetadata { get; set; }

  /// <summary>
  /// A prefix to add to the names of resources created by the Kustomization.
  /// </summary>
  public string? NamePrefix { get; set; }

  /// <summary>
  /// A suffix to add to the names of resources created by the Kustomization.
  /// </summary>
  public string? NameSuffix { get; set; }

  /// <summary>
  /// A list of patches to apply to some of the Kustomization's resources.
  /// </summary>
  public IEnumerable<FluxPatch>? Patches { get; set; }

  /// <summary>
  /// A list of images to replace if encountered in the Kustomization's resources.
  /// </summary>
  public IEnumerable<FluxImage>? Images { get; set; }

  /// <summary>
  /// A list of components to apple to the Kustomization's resources.
  /// </summary>
  public IEnumerable<string>? Components { get; set; }

  /// <summary>
  /// Post build operations to perform after the Kustomization has been applied.
  /// </summary>
  public FluxKustomizationSpecPostBuild? PostBuild { get; set; }

  /// <summary>
  /// Whether to force replacing the Kustomization's resources.
  /// </summary>
  public bool? Force { get; set; }

  /// <summary>
  /// The kubeconfig to use for the reconciliation of the Kustomization.
  /// </summary>
  public FluxKubeConfig? KubeConfig { get; set; }

  /// <summary>
  /// The configuration to decrypt secrets in the Kustomization.
  /// </summary>
  public FluxKustomizationSpecDecryption? Decryption { get; set; }
}
