using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.KSail.Models.Registry;

/// <summary>
/// The registry provider.
/// </summary>
public enum KSailRegistryProvider
{
  /// <summary>
  /// The Docker registry provider.
  /// </summary>
  [EnumMember(Value = "docker")]
  Docker
}
