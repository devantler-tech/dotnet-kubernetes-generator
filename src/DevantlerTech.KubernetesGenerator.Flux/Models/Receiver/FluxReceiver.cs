using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

/// <summary>
/// Represents a Flux Receiver resource.
/// </summary>
public class FluxReceiver
{
  /// <summary>
  /// Gets or sets the API version.
  /// </summary>
  public string ApiVersion { get; set; } = "notification.toolkit.fluxcd.io/v1";

  /// <summary>
  /// Gets or sets the kind.
  /// </summary>
  public string Kind { get; set; } = "Receiver";

  /// <summary>
  /// Gets or sets the metadata.
  /// </summary>
  public required Metadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the spec.
  /// </summary>
  public required FluxReceiverSpec Spec { get; set; }
}