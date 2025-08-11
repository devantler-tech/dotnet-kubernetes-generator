namespace DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

/// <summary>
/// Represents a resource reference for a Flux Receiver.
/// </summary>
public class FluxReceiverResource
{
  /// <summary>
  /// Gets or sets the kind of the resource.
  /// </summary>
  public required FluxCustomResourceKind Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the resource.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the resource.
  /// </summary>
  public string? Namespace { get; set; }
}