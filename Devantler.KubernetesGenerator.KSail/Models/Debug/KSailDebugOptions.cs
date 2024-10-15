namespace Devantler.KubernetesGenerator.KSail.Models.Debug;

/// <summary>
/// The options to use for the 'debug' command.
/// </summary>
public class KSailDebugOptions
{
  /// <summary>
  /// The editor to use for viewing files while debugging.
  /// </summary>
  public DebugEditor? Editor { get; set; }
}

/// <summary>
/// The editor to use for viewing files while debugging.
/// </summary>
public enum DebugEditor
{
  /// <summary>
  /// Visual Studio Code
  /// </summary>
  VisualStudioCode,
  /// <summary>
  /// Nano
  /// </summary>
  Nano,
  /// <summary>
  /// Vim
  /// </summary>
  Vim
}
