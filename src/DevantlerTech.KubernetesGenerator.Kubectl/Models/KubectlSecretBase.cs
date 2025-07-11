namespace DevantlerTech.KubernetesGenerator.Kubectl.Models;

/// <summary>
/// Base model for kubectl secret generation.
/// </summary>
public abstract class KubectlSecretBase
{
  /// <summary>
  /// The name of the secret.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// The namespace of the secret.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// Labels to apply to the secret.
  /// </summary>
  public Dictionary<string, string>? Labels { get; init; }

  /// <summary>
  /// Annotations to apply to the secret.
  /// </summary>
  public Dictionary<string, string>? Annotations { get; init; }

  /// <summary>
  /// Whether to append a hash of the secret to its name.
  /// </summary>
  public bool AppendHash { get; set; }
}