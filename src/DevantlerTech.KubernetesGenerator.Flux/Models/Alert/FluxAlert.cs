using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.Alert;

/// <summary>
/// A Flux Alert.
/// </summary>
public class FluxAlert
{
  /// <summary>
  /// Metadata of the Alert.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Spec of the Alert.
  /// </summary>
  public required FluxAlertSpec Spec { get; set; }
}
