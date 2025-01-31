namespace Devantler.KubernetesGenerator.Flux.Models.AlertProvider;

/// <summary>
/// The spec of a Flux Alert Provider.
/// </summary>
public class FluxAlertProviderSpec
{
  /// <summary>
  /// The type of the alert provider.
  /// </summary>
  public required FluxAlertProviderType Type { get; set; }

  /// <summary>
  /// Path to either the git repository, chat provider or webhook.
  /// </summary>
  public string? Address { get; set; }

  /// <summary>
  /// Channel to send messages to in the case of a chat provider.
  /// </summary>
  public string? Channel { get; set; }

  /// <summary>
  /// Name of Secret containing authentication token
  /// </summary>
  public FluxSecretRef? SecretRef { get; set; }

  /// <summary>
  /// Bot username used by the provider.
  /// </summary>
  public string? Username { get; set; }
}
