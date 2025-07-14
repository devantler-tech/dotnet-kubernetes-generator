using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a PersistentVolume for use with kubectl create commands.
/// </summary>
public class PersistentVolume(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the persistent volume.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the access modes for the persistent volume.
  /// </summary>
  public IList<string>? AccessModes { get; init; }

  /// <summary>
  /// Gets or sets the storage capacity of the persistent volume.
  /// </summary>
  public ResourceQuantity? Capacity { get; init; }

  /// <summary>
  /// Gets or sets the reclaim policy for the persistent volume.
  /// </summary>
  public string? ReclaimPolicy { get; init; }

  /// <summary>
  /// Gets or sets the storage class name for the persistent volume.
  /// </summary>
  public string? StorageClassName { get; init; }

  /// <summary>
  /// Gets or sets the volume mode for the persistent volume.
  /// </summary>
  public string? VolumeMode { get; init; }

  /// <summary>
  /// Gets or sets the mount options for the persistent volume.
  /// </summary>
  public IList<string>? MountOptions { get; init; }

  /// <summary>
  /// Gets or sets the host path configuration for the persistent volume.
  /// </summary>
  public HostPathVolumeSource? HostPath { get; init; }

  /// <summary>
  /// Gets or sets the NFS configuration for the persistent volume.
  /// </summary>
  public NfsVolumeSource? Nfs { get; init; }

  /// <summary>
  /// Gets or sets the local storage configuration for the persistent volume.
  /// </summary>
  public LocalVolumeSource? Local { get; init; }

  /// <summary>
  /// Gets or sets the node affinity for the persistent volume.
  /// </summary>
  public NodeAffinity? NodeAffinity { get; init; }
}

/// <summary>
/// Represents host path volume source configuration.
/// </summary>
public class HostPathVolumeSource
{
  /// <summary>
  /// Gets or sets the path on the host.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets the type of the host path.
  /// </summary>
  public string? Type { get; set; }
}

/// <summary>
/// Represents NFS volume source configuration.
/// </summary>
public class NfsVolumeSource
{
  /// <summary>
  /// Gets or sets the NFS server address.
  /// </summary>
  public required string Server { get; set; }

  /// <summary>
  /// Gets or sets the path on the NFS server.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets whether the NFS mount is read-only.
  /// </summary>
  public bool ReadOnly { get; set; }
}

/// <summary>
/// Represents local volume source configuration.
/// </summary>
public class LocalVolumeSource
{
  /// <summary>
  /// Gets or sets the path on the local node.
  /// </summary>
  public required string Path { get; set; }

  /// <summary>
  /// Gets or sets the file system type.
  /// </summary>
  public string? FsType { get; set; }
}

/// <summary>
/// Represents node affinity configuration.
/// </summary>
public class NodeAffinity
{
  /// <summary>
  /// Gets or sets the required node selector terms.
  /// </summary>
  public IList<NodeSelectorTerm>? Required { get; init; }
}

/// <summary>
/// Represents a node selector term.
/// </summary>
public class NodeSelectorTerm
{
  /// <summary>
  /// Gets or sets the match expressions for the node selector.
  /// </summary>
  public IList<NodeSelectorRequirement>? MatchExpressions { get; init; }

  /// <summary>
  /// Gets or sets the match fields for the node selector.
  /// </summary>
  public IList<NodeSelectorRequirement>? MatchFields { get; init; }
}

/// <summary>
/// Represents a node selector requirement.
/// </summary>
public class NodeSelectorRequirement
{
  /// <summary>
  /// Gets or sets the key for the requirement.
  /// </summary>
  public required string Key { get; set; }

  /// <summary>
  /// Gets or sets the operator for the requirement.
  /// </summary>
  public required string Operator { get; set; }

  /// <summary>
  /// Gets or sets the values for the requirement.
  /// </summary>
  public IList<string>? Values { get; init; }
}