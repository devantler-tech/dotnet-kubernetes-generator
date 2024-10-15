using Devantler.KubernetesGenerator.KSail.Models.Check;
using Devantler.KubernetesGenerator.KSail.Models.Debug;
using Devantler.KubernetesGenerator.KSail.Models.Down;
using Devantler.KubernetesGenerator.KSail.Models.Gen;
using Devantler.KubernetesGenerator.KSail.Models.Init;
using Devantler.KubernetesGenerator.KSail.Models.Lint;
using Devantler.KubernetesGenerator.KSail.Models.List;
using Devantler.KubernetesGenerator.KSail.Models.Registry;
using Devantler.KubernetesGenerator.KSail.Models.Sops;
using Devantler.KubernetesGenerator.KSail.Models.Start;
using Devantler.KubernetesGenerator.KSail.Models.Stop;
using Devantler.KubernetesGenerator.KSail.Models.Up;
using Devantler.KubernetesGenerator.KSail.Models.Update;

namespace Devantler.KubernetesGenerator.KSail.Models;

/// <summary>
/// The KSail cluster specification.
/// </summary>
public class KSailClusterSpec
{
  /// <summary>
  /// The path to the kubeconfig file.
  /// </summary>
  public string? Kubeconfig { get; set; }

  /// <summary>
  /// The kube context.
  /// </summary>
  public string? Context { get; set; }

  /// <summary>
  /// The timeout for operations (in seconds).
  /// </summary>
  public int? Timeout { get; set; }

  /// <summary>
  /// The path to the directory that contains the manifests.
  /// </summary>
  public string? ManifestsDirectory { get; set; }

  /// <summary>
  /// The relative path to the directory that contains the root kustomization file.
  /// </summary>
  public string? KustomizationDirectory { get; set; }

  /// <summary>
  /// The path to the configuration file.
  /// </summary>
  public string? ConfigPath { get; set; }

  /// <summary>
  /// The Kubernetes distribution to use.
  /// </summary>
  public KSailKubernetesDistribution? Distribution { get; set; }

  /// <summary>
  /// The GitOps tool to use.
  /// </summary>
  public KSailGitOpsTool? GitOpsTool { get; set; }

  /// <summary>
  /// The container engine to use.
  /// </summary>
  public KSailContainerEngine? ContainerEngine { get; set; }

  /// <summary>
  /// Whether to enable SOPS support.
  /// </summary>
  public bool? Sops { get; set; }

  /// <summary>
  /// The registries to create for the KSail cluster to reconcile flux artifacts, and to proxy and cache images.
  /// </summary>
  public IEnumerable<KSailRegistry>? Registries { get; set; }

  /// <summary>
  /// The options to use for the 'check' command.
  /// </summary>
  public KSailCheckOptions? CheckOptions { get; set; }

  /// <summary>
  /// The options to use for the 'debug' command.
  /// </summary>
  public KSailDebugOptions? DebugOptions { get; set; }

  /// <summary>
  /// The options to use for the 'down' command.
  /// </summary>
  public KSailDownOptions? DownOptions { get; set; }

  /// <summary>
  /// The options to use for the 'gen' command.
  /// </summary>
  public KSailGenOptions? GenOptions { get; set; }

  /// <summary>
  /// The options to use for the 'init' command.
  /// </summary>
  public KSailInitOptions? InitOptions { get; set; }

  /// <summary>
  /// The options to use for the 'lint' command.
  /// </summary>
  public KSailLintOptions? LintOptions { get; set; }

  /// <summary>
  /// The options to use for the 'list' command.
  /// </summary>
  public KSailListOptions? ListOptions { get; set; }

  /// <summary>
  /// The options to use for the 'sops' command.
  /// </summary>
  public KSailSopsOptions? SopsOptions { get; set; }

  /// <summary>
  /// The options to use for the 'start' command.
  /// </summary>
  public KSailStartOptions? StartOptions { get; set; }

  /// <summary>
  /// The options to use for the 'stop' command.
  /// </summary>
  public KSailStopOptions? StopOptions { get; set; }

  /// <summary>
  /// The options to use for the 'up' command.
  /// </summary>
  public KSailUpOptions? UpOptions { get; set; }

  /// <summary>
  /// The options to use for the 'update' command.
  /// </summary>
  public KSailUpdateOptions? UpdateOptions { get; set; }
}
