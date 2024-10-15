namespace Devantler.KubernetesGenerator.KSail.Models.Down;

/// <summary>
/// The options to use for the 'down' command.
/// </summary>
public class KSailDownOptions
{
  /// <summary>
  /// Whether to remove ksail registries (will remove all cached images).
  /// </summary>
  public bool? Registries { get; set; }
}
