namespace DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

/// <summary>
/// Represents the spec of a Flux Receiver.
/// </summary>
public class FluxReceiverSpec
{
  /// <summary>
  /// Gets or sets the type of the receiver.
  /// </summary>
  public required FluxReceiverType Type { get; set; }

  /// <summary>
  /// Gets or sets the events that trigger the receiver.
  /// </summary>
  public IEnumerable<FluxReceiverEvent>? Events { get; set; }

  /// <summary>
  /// Gets or sets the secret reference for webhook authentication.
  /// </summary>
  public FluxSecretRef? SecretRef { get; set; }

  /// <summary>
  /// Gets or sets the resources that should be triggered by the receiver.
  /// </summary>
  public IEnumerable<FluxReceiverResource>? Resources { get; set; }
}