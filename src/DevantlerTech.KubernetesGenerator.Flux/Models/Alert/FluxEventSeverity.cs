using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.Alert;

/// <summary>
/// The event severity of a Flux Alert.
/// </summary>
public enum FluxEventSeverity
{
  /// <summary>
  /// Info event severity.
  /// </summary>
  [YamlMember(Alias = "info")]
  Info,

  /// <summary>
  /// Error event severity.
  /// </summary>
  [YamlMember(Alias = "error")]
  Error
}
