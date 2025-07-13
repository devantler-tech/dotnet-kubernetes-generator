namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a subject for role binding operations.
/// </summary>
public class Subject
{
  /// <summary>
  /// Gets or sets the kind of the subject (User, Group, ServiceAccount).
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the subject.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the API group of the subject.
  /// </summary>
  public string? ApiGroup { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the subject (only applicable for ServiceAccount).
  /// </summary>
  public string? Namespace { get; set; }
}