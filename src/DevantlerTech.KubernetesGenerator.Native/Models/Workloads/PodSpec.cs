namespace DevantlerTech.KubernetesGenerator.Native.Models.Workloads;

/// <summary>
/// Represents the specification for a Pod.
/// </summary>
public class PodSpec
{
  /// <summary>
  /// Gets or sets the containers in the pod.
  /// </summary>
  public required IList<PodContainer> Containers { get; init; }

  /// <summary>
  /// Gets or sets the restart policy for all containers within the pod.
  /// </summary>
  public PodRestartPolicy? RestartPolicy { get; set; }

  /// <summary>
  /// Gets or sets whether this container should allocate a TTY for itself.
  /// </summary>
  public bool? Tty { get; set; }

  /// <summary>
  /// Gets or sets whether this container should allocate a buffer for stdin in the container runtime.
  /// </summary>
  public bool? Stdin { get; set; }

  /// <summary>
  /// Gets or sets whether the container runtime should close the stdin channel after it has been opened by a single attach.
  /// When stdin is true the stdin stream will remain open across multiple attach sessions. If stdinOnce is set to true, stdin is opened on container start, is empty until the first client attaches to stdin, and then remains open and accepts data until the client disconnects, at which time stdin is closed and remains closed until the container is restarted.
  /// </summary>
  public bool? StdinOnce { get; set; }
}
