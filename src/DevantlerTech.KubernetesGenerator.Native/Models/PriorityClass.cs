using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a priority class for use with kubectl create priorityclass.
/// </summary>
public class PriorityClass
{
  /// <summary>
  /// Gets or sets the metadata for the priority class.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the value of this priority class. This is the actual priority that pods receive when they have the name of this class in their pod spec.
  /// </summary>
  public required int Value { get; set; }

  /// <summary>
  /// Gets or sets the description of this priority class.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// Gets or sets whether this priority class should be considered as the default priority. Only one priority class can be marked as global default.
  /// </summary>
  public bool? GlobalDefault { get; set; }

  /// <summary>
  /// Gets or sets the preemption policy for this priority class.
  /// </summary>
  public string? PreemptionPolicy { get; set; }
}