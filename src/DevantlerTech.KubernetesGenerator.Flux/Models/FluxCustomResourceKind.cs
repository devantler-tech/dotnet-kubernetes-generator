namespace DevantlerTech.KubernetesGenerator.Flux.Models;

/// <summary>
/// The kind of a flux custom resource.
/// </summary>
public enum FluxCustomResourceKind
{
  /// <summary>
  /// HelmRelease kind.
  /// </summary>
  HelmRelease,

  /// <summary>
  /// Kustomization kind.
  /// </summary>
  Kustomization,

  /// <summary>
  /// Alert kind.
  /// </summary>
  Alert,

  /// <summary>
  /// Provider kind.
  /// </summary>
  Provider,

  /// <summary>
  /// Receiver kind.
  /// </summary>
  Receiver,

  /// <summary>
  /// Bucket kind.
  /// </summary>
  Bucket,

  /// <summary>
  /// GitRepository kind.
  /// </summary>
  GitRepository,

  /// <summary>
  /// HelmChart kind.
  /// </summary>
  HelmChart,

  /// <summary>
  /// HelmRepository kind.
  /// </summary>
  HelmRepository,

  /// <summary>
  /// OCIRepository kind.
  /// </summary>
  OCIRepository,
}
