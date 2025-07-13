using System.Collections.ObjectModel;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents a Kubernetes generic Secret for use with kubectl create secret generic.
/// </summary>
public class GenericSecret
{
  /// <summary>
  /// Gets or sets the metadata for the secret.
  /// </summary>
  public required ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Gets or sets the type of the secret.
  /// </summary>
  public string? Type { get; set; }

  /// <summary>
  /// Gets or sets the data for the secret as key-value pairs.
  /// </summary>
  public Dictionary<string, string>? Data { get; init; }

  /// <summary>
  /// Gets or sets the files to load data from.
  /// </summary>
  public Collection<string>? FromFiles { get; init; }

  /// <summary>
  /// Gets or sets the environment files to load data from.
  /// </summary>
  public Collection<string>? FromEnvFiles { get; init; }

  /// <summary>
  /// Gets or sets the literal key-value pairs to add.
  /// </summary>
  public Dictionary<string, string>? FromLiterals { get; init; }
}