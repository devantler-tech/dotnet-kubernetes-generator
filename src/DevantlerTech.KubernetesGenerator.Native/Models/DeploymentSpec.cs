namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the specification for a Deployment.
/// </summary>
public class DeploymentSpec
{
  /// <summary>
  /// Gets or sets the number of desired replicas.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the list of container images to run.
  /// </summary>
  public required IReadOnlyList<string> Images { get; init; }

  /// <summary>
  /// Gets or sets the port that this deployment exposes.
  /// </summary>
  public int? Port { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public IReadOnlyList<string>? Command { get; init; }
}