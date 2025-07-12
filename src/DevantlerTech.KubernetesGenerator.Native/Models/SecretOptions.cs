namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Base class for secret creation options.
/// </summary>
public abstract class SecretOptions
{
  /// <summary>
  /// The name of the secret.
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// The namespace for the secret.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// Whether to append a hash to the secret name.
  /// </summary>
  public bool AppendHash { get; set; }

  /// <summary>
  /// Output format for the secret.
  /// </summary>
  public string? Output { get; set; }

  /// <summary>
  /// Dry run mode.
  /// </summary>
  public string? DryRun { get; set; }

  /// <summary>
  /// Save configuration in annotation.
  /// </summary>
  public bool SaveConfig { get; set; }

  /// <summary>
  /// Validation mode.
  /// </summary>
  public string? Validate { get; set; }

  /// <summary>
  /// Template for output formatting.
  /// </summary>
  public string? Template { get; set; }

  /// <summary>
  /// Show managed fields.
  /// </summary>
  public bool ShowManagedFields { get; set; }

  /// <summary>
  /// Field manager name.
  /// </summary>
  public string? FieldManager { get; set; }

  /// <summary>
  /// Allow missing template keys.
  /// </summary>
  public bool AllowMissingTemplateKeys { get; set; } = true;
}
