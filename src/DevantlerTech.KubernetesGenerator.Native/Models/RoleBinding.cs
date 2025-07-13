using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a role binding for use with kubectl create rolebinding.
/// </summary>
public class RoleBinding
{
  /// <summary>
  /// Gets or sets the metadata for the role binding.
  /// </summary>
  public V1ObjectMeta? Metadata { get; set; }

  /// <summary>
  /// Gets or sets the reference to the role or cluster role.
  /// </summary>
  public required RoleRef RoleRef { get; set; }

  /// <summary>
  /// Gets or sets the subjects that are bound to the role.
  /// </summary>
  public required IList<Subject> Subjects { get; init; }
}

/// <summary>
/// Represents a reference to a role or cluster role.
/// </summary>
public class RoleRef
{
  /// <summary>
  /// Gets or sets the kind of role reference (Role or ClusterRole).
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the role.
  /// </summary>
  public required string Name { get; set; }
}

/// <summary>
/// Represents a subject that can be bound to a role.
/// </summary>
public class Subject
{
  /// <summary>
  /// Gets or sets the kind of subject (User, Group, or ServiceAccount).
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// Gets or sets the name of the subject.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Gets or sets the namespace of the subject (optional, only applicable for ServiceAccount).
  /// </summary>
  public string? Namespace { get; set; }
}