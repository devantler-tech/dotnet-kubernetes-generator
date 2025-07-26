namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents node affinity for a persistent volume.
/// </summary>
public class PersistentVolumeNodeAffinity
{
  /// <summary>
  /// Gets or sets the required node selector.
  /// </summary>
  public PersistentVolumeNodeSelector? Required { get; set; }
}

/// <summary>
/// Represents a node selector for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeSelector
{
  /// <summary>
  /// Gets or sets the node selector terms.
  /// </summary>
  public IList<PersistentVolumeNodeSelectorTerm>? NodeSelectorTerms { get; init; }
}

/// <summary>
/// Represents a node selector term for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeSelectorTerm
{
  /// <summary>
  /// Gets or sets the match expressions.
  /// </summary>
  public IList<PersistentVolumeNodeSelectorRequirement>? MatchExpressions { get; init; }
}

/// <summary>
/// Represents a node selector requirement for persistent volume node affinity.
/// </summary>
public class PersistentVolumeNodeSelectorRequirement
{
  /// <summary>
  /// Gets or sets the key.
  /// </summary>
  public required string Key { get; set; }

  /// <summary>
  /// Gets or sets the operator.
  /// </summary>
  public required string Operator { get; set; }

  /// <summary>
  /// Gets or sets the values.
  /// </summary>
  public IList<string>? Values { get; init; }
}