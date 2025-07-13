namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a policy rule for use with kubectl create clusterrole.
/// </summary>
public class PolicyRule
{
  /// <summary>
  /// Gets or sets the verbs that apply to the resources contained in this rule.
  /// </summary>
  public required IEnumerable<string> Verbs { get; set; }

  /// <summary>
  /// Gets or sets the API groups that contain the resources.
  /// </summary>
  public IEnumerable<string>? ApiGroups { get; set; }

  /// <summary>
  /// Gets or sets the resources that this rule applies to.
  /// </summary>
  public IEnumerable<string>? Resources { get; set; }

  /// <summary>
  /// Gets or sets the names of the resources that this rule applies to.
  /// </summary>
  public IEnumerable<string>? ResourceNames { get; set; }

  /// <summary>
  /// Gets or sets the non-resource URLs that this rule applies to.
  /// </summary>
  public IEnumerable<string>? NonResourceURLs { get; set; }
}