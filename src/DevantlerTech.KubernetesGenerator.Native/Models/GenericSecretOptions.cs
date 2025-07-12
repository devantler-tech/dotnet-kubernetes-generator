using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Options for creating a generic secret.
/// </summary>
public class GenericSecretOptions : SecretOptions
{
  /// <summary>
  /// The type of the secret.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Files to create the secret from.
  /// </summary>
  public Collection<string> FromFiles { get; } = [];

  /// <summary>
  /// Literal key-value pairs to create the secret from.
  /// </summary>
  public Dictionary<string, string> FromLiterals { get; } = [];

  /// <summary>
  /// Environment files to create the secret from.
  /// </summary>
  public Collection<string> FromEnvFiles { get; } = [];
}
