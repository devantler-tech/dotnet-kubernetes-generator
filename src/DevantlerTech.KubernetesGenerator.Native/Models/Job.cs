using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Job for use with kubectl create job.
/// </summary>
public class Job(string name, string image)
{
  /// <summary>
  /// Gets or sets the metadata for the job.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the container image to run.
  /// </summary>
  public required string Image { get; set; } = image;

  /// <summary>
  /// Gets the command to run in the container.
  /// </summary>
  public Collection<string> Command { get; } = [];

  /// <summary>
  /// Gets the arguments to pass to the command.
  /// </summary>
  public Collection<string> Args { get; } = [];

  /// <summary>
  /// Gets or sets the restart policy for the job.
  /// </summary>
  public string? RestartPolicy { get; set; }
}