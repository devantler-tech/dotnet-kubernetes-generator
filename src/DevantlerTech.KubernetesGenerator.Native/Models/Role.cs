namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Role for use with kubectl create role.
/// </summary>
public class Role(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the role.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the rules for the role.
  /// </summary>
  public required IList<Rule> Rules { get; init; }
}