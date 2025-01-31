using System.Text.Json.Serialization;

namespace Devantler.KubernetesGenerator.Kind.Models;

/// <summary>
/// A set of Kubernetes features that can be enabled or disabled.
/// </summary>
public class KindFeatureGates
{
  /// <summary>
  /// Enable or disable the CSIMigration feature.
  /// </summary>
  [JsonPropertyName("CSIMigration")]
  public bool CSIMigration { get; set; }
}
