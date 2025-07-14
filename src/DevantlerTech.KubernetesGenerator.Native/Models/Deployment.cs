namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Deployment for use with kubectl create deployment.
/// </summary>
public class Deployment(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the deployment.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets the container images for the deployment.
  /// Multiple images can be specified for multi-container pods.
  /// </summary>
  public List<string> Images { get; } = [];

  /// <summary>
  /// Gets or sets the number of replicas for the deployment.
  /// Defaults to 1 if not specified.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the container port that this deployment exposes.
  /// </summary>
  public int? Port { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public string? Command { get; set; }

  /// <summary>
  /// Gets the arguments to pass to the command.
  /// </summary>
  public List<string> Args { get; } = [];
}