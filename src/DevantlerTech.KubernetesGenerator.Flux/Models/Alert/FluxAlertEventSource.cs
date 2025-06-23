namespace DevantlerTech.KubernetesGenerator.Flux.Models.Alert;

/// <summary>
/// The event source of a Flux Alert.
/// </summary>
public class FluxAlertEventSource
{
  /// <summary>
  /// The flux custom resource kind of the event source.
  /// </summary>
  public required FluxCustomResourceKind Kind { get; set; }

  /// <summary>
  /// The name of the event source.
  /// </summary>
  public required string Name { get; set; }
}
