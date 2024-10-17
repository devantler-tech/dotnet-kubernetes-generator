using Devantler.K9sCLI;

namespace Devantler.KubernetesGenerator.KSail.Models.Debug;

/// <summary>
/// The options to use for the 'debug' command.
/// </summary>
public class KSailDebugOptions
{
  /// <summary>
  /// The editor to use for viewing files while debugging.
  /// </summary>
  public Editor? Editor { get; set; }
}
