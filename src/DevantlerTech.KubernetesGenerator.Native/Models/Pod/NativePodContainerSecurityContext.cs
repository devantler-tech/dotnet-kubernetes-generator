namespace DevantlerTech.KubernetesGenerator.Native.Models.Pod;

/// <summary>
/// Represents the security context for a container.
/// </summary>
public class NativePodContainerSecurityContext
{
  /// <summary>
  /// Gets or sets whether to run this container in privileged mode.
  /// </summary>
  public bool? Privileged { get; set; }

  /// <summary>
  /// Gets or sets the user ID to run the entrypoint of the container process.
  /// </summary>
  public long? RunAsUser { get; set; }

  /// <summary>
  /// Gets or sets the group ID to run the entrypoint of the container process.
  /// </summary>
  public long? RunAsGroup { get; set; }

  /// <summary>
  /// Gets or sets whether the container must run as a non-root user.
  /// </summary>
  public bool? RunAsNonRoot { get; set; }

  /// <summary>
  /// Gets or sets whether the container's root filesystem will be read-only.
  /// </summary>
  public bool? ReadOnlyRootFilesystem { get; set; }

  /// <summary>
  /// Gets or sets whether the container should allow privilege escalation.
  /// </summary>
  public bool? AllowPrivilegeEscalation { get; set; }
}
