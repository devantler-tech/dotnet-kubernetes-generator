namespace Devantler.KubernetesGenerator.Flux.Models.Alert;

/// <summary>
/// The provider reference of a Flux Alert.
/// </summary>
public class FluxAlertProviderRef
{
  /// <summary>
  /// The name of the provider.
  /// </summary>
  public required string Name { get; set; }
}
