namespace Devantler.KubernetesGenerator.KSail.Models.Up;

/// <summary>
/// The options to use for the 'up' command.
/// </summary>
public class KSailUpOptions
{
  /// <summary>
  /// Whether to lint the manifests before applying them.
  /// </summary>
  public bool? Lint { get; set; }

  /// <summary>
  /// Whether to wait for reconciliation to succeed.
  /// </summary>
  public bool? Reconcile { get; set; }
}
