using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Pod for use with kubectl run command.
/// </summary>
public class Pod(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the pod.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the container image.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the command to run in the container.
  /// </summary>
  public Collection<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the arguments to pass to the command.
  /// </summary>
  public Collection<string>? Args { get; init; }

  /// <summary>
  /// Gets or sets the environment variables for the container.
  /// </summary>
  public Dictionary<string, string>? Environment { get; init; }

  /// <summary>
  /// Gets or sets the port to expose on the container.
  /// </summary>
  public int? Port { get; set; }

  /// <summary>
  /// Gets or sets the restart policy for the pod.
  /// </summary>
  public string? RestartPolicy { get; set; }

  /// <summary>
  /// Gets or sets the labels for the pod.
  /// </summary>
  public Dictionary<string, string>? Labels { get; init; }
}