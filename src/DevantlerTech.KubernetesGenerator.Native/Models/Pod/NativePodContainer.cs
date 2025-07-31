namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents a container in a Pod.
/// </summary>
public class NativePodContainer
{
  /// <summary>
  /// Gets or sets the name of the container.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the Docker image name.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets or sets the image pull policy.
  /// </summary>
  public NativePodImagePullPolicy? ImagePullPolicy { get; set; }

  /// <summary>
  /// Gets or sets the entrypoint array. Not executed within a shell.
  /// </summary>
  public IList<string>? Command { get; init; }

  /// <summary>
  /// Gets or sets the arguments to the entrypoint.
  /// </summary>
  public IList<string>? Args { get; init; }

  /// <summary>
  /// Gets or sets the list of environment variables to set in the container.
  /// </summary>
  public IList<NativePodContainerEnvVar>? Env { get; init; }

  /// <summary>
  /// Gets or sets the list of ports to expose from the container.
  /// </summary>
  public IList<NativePodContainerPort>? Ports { get; init; }

  /// <summary>
  /// Gets or sets the security context for this container.
  /// </summary>
  public NativePodContainerSecurityContext? SecurityContext { get; set; }
}
