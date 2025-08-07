namespace DevantlerTech.KubernetesGenerator.Flux.Models.Receiver;

/// <summary>
/// Represents an event type for a Flux Receiver.
/// </summary>
public enum FluxReceiverEvent
{
  /// <summary>
  /// Ping event.
  /// </summary>
  Ping,

  /// <summary>
  /// Push event.
  /// </summary>
  Push,

  /// <summary>
  /// Tag push event.
  /// </summary>
  TagPush,

  /// <summary>
  /// Pull request event.
  /// </summary>
  PullRequest,

  /// <summary>
  /// Release event.
  /// </summary>
  Release
}