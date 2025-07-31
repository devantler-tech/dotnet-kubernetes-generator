namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

/// <summary>
/// Represents an environment variable in a container.
/// </summary>
public class PodContainerEnvVar
{
  /// <summary>
  /// Gets or sets the name of the environment variable.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the value of the environment variable.
  /// </summary>
  public required string Value { get; set; }
}
