namespace DevantlerTech.KubernetesGenerator.Flux.Models.AlertProvider;

/// <summary>
/// A Flux Alert Provider.
/// </summary>
public class FluxAlertProvider
{
  /// <summary>
  /// Metadata of the Alert Provider.
  /// </summary>
  public required FluxNamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Spec of the Alert Provider.
  /// </summary>
  public required FluxAlertProviderSpec Spec { get; set; }
}
