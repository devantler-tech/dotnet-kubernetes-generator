namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a role for use with kubectl create role.
/// </summary>
public class Role(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the role.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the list of policy rules for the role.
  /// </summary>
  public required IList<RolePolicyRule> Rules { get; init; }
}