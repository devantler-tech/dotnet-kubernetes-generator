namespace DevantlerTech.KubernetesGenerator.Core.Models;

/// <summary>
/// Represents metadata for cluster-scoped resources that don't have a namespace.
/// </summary>
public class ClusterScopedMetadata : Metadata
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ClusterScopedMetadata"/> class.
  /// </summary>
  public ClusterScopedMetadata()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ClusterScopedMetadata"/> class.
  /// </summary>
  /// <param name="labels">The labels to set on the metadata.</param>
  public ClusterScopedMetadata(IDictionary<string, string>? labels) => Labels = labels;
}
