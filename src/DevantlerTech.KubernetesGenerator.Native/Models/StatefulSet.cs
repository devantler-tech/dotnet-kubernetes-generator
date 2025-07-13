using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a StatefulSet for use with kubectl create deployment commands.
/// </summary>
public class StatefulSet
{
  /// <summary>
  /// Gets or sets the metadata for the StatefulSet.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the number of replicas for the StatefulSet.
  /// </summary>
  public int? Replicas { get; set; }

  /// <summary>
  /// Gets or sets the service name for the StatefulSet.
  /// </summary>
  public required string ServiceName { get; set; }

  /// <summary>
  /// Gets the containers for the StatefulSet.
  /// </summary>
  public required IList<StatefulSetContainer> Containers { get; init; }
}

/// <summary>
/// Represents a container in a StatefulSet.
/// </summary>
public class StatefulSetContainer
{
  /// <summary>
  /// Gets or sets the name of the container.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the image of the container.
  /// </summary>
  public required string Image { get; set; }

  /// <summary>
  /// Gets the command for the container.
  /// </summary>
  public IList<string>? Command { get; init; }
}