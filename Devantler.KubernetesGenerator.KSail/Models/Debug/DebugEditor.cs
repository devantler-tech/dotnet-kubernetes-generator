using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.KSail.Models.Debug;

/// <summary>
/// The editor to use for viewing files while debugging.
/// </summary>
public enum DebugEditor
{
  /// <summary>
  /// Visual Studio Code
  /// </summary>
  [EnumMember(Value = "vscode")]
  VisualStudioCode,
  /// <summary>
  /// Nano
  /// </summary>
  [EnumMember(Value = "nano")]
  Nano,
  /// <summary>
  /// Vim
  /// </summary>
  [EnumMember(Value = "vim")]
  Vim
}
