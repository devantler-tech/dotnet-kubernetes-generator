namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a label selector for use with kubectl create clusterrole.
/// </summary>
public class LabelSelector
{
  /// <summary>
  /// Gets the match labels for the selector.
  /// </summary>
  public Dictionary<string, string> MatchLabels { get; } = [];
}