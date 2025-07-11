namespace DevantlerTech.KubernetesGenerator.Kubectl.Models;

/// <summary>
/// Model for kubectl generic secret generation.
/// </summary>
public class KubectlGenericSecret : KubectlSecretBase
{
  /// <summary>
  /// The type of the secret (defaults to Opaque).
  /// </summary>
  public string Type { get; set; } = "Opaque";

  /// <summary>
  /// Literal key-value pairs to include in the secret.
  /// </summary>
  public Dictionary<string, string>? FromLiteral { get; init; }

  /// <summary>
  /// Files to include in the secret.
  /// Key is the secret key name, Value is the file path.
  /// </summary>
  public Dictionary<string, string>? FromFile { get; init; }

  /// <summary>
  /// Environment files to include in the secret.
  /// </summary>
  public IReadOnlyCollection<string>? FromEnvFile { get; init; }
}