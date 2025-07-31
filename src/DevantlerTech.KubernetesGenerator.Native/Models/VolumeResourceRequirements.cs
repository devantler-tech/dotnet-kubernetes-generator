namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents resource requirements for a volume.
/// </summary>
public class VolumeResourceRequirements
{
  /// <summary>
  /// Gets or sets the minimum amount of resources required.
  /// </summary>
  public IDictionary<string, string>? Requests { get; init; }

  /// <summary>
  /// Gets or sets the maximum amount of resources allowed.
  /// </summary>
  public IDictionary<string, string>? Limits { get; init; }
}
