namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes Namespace for use with kubectl create namespace.
/// </summary>
public class Namespace(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the namespace.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };
}