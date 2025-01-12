namespace Devantler.KubernetesGenerator.Flux.Models.Alert;

/// <summary>
/// The spec of a Flux Alert.
/// </summary>
public class FluxAlertSpec
{
  /// <summary>
  /// The event sources of the alert.
  /// </summary>
  public required IEnumerable<FluxAlertEventSource> EventSources { get; set; }

  /// <summary>
  /// The provider reference of the alert.
  /// </summary>
  public required FluxAlertProviderRef ProviderRef { get; set; }

  /// <summary>
  /// The event severity of the alert.
  /// </summary>
  public FluxEventSeverity? EventSeverity { get; set; }
}
