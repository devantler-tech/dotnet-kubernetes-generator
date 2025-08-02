namespace DevantlerTech.KubernetesGenerator.Native.Models.Secret;

/// <summary>
/// Represents a generic secret for use with kubectl create secret generic.
/// </summary>
public class NativeGenericSecret : NativeSecret
{
  /// <summary>
  /// Gets or sets the secret type. If not specified, defaults to 'Opaque'.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Gets the secret data as key-value pairs.
  /// </summary>
  public Dictionary<string, string> Data { get; } = [];
}
