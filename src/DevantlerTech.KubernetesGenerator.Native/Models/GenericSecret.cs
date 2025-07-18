namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a generic secret for use with kubectl create secret generic.
/// </summary>
public class GenericSecret(string name)
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public Metadata Metadata { get; set; } = new() { Name = name };

  /// <summary>
  /// Gets or sets the secret type. If not specified, defaults to 'Opaque'.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Gets the secret data as key-value pairs.
  /// </summary>
  public Dictionary<string, string> Data { get; } = [];
}
