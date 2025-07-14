namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a reference to the target object to scale.
/// </summary>
public class ScaleTargetRef
{
  /// <summary>
  /// Gets or sets the API version of the target object.
  /// </summary>
  public string? ApiVersion { get; set; }

  /// <summary>
  /// Gets or sets the kind of the target object.
  /// </summary>
  public string? Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the target object.
  /// </summary>
  public string? Name { get; set; }
}