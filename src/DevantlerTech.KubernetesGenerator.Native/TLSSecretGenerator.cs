using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for TLS Kubernetes Secret objects using 'kubectl create secret tls' commands.
/// </summary>
public class TLSSecretGenerator : BaseNativeGenerator<TLSSecret>
{
  /// <summary>
  /// Generates a TLS secret using kubectl create secret tls command.
  /// </summary>
  /// <param name="model">The TLS secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(TLSSecret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    string errorMessage = $"Failed to create TLS secret '{model.Metadata?.Name}' using kubectl";

    await RunKubectlAsync(outputPath, overwrite, await AddArgumentsAsync(model, cancellationToken).ConfigureAwait(false), errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a TLS secret from a TLSSecret object.
  /// </summary>
  /// <param name="model">The TLSSecret object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The kubectl arguments.</returns>
  static async Task<ReadOnlyCollection<string>> AddArgumentsAsync(TLSSecret model, CancellationToken cancellationToken = default)
  {
    var args = new List<string> { "create", "secret", "tls" };
    
    // Require that a secret name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the secret name.");
    }
    args.Add(model.Metadata.Name);

    // Handle certificate and key data
    string certPath = await GetCertificatePathAsync(model, cancellationToken).ConfigureAwait(false);
    string keyPath = await GetPrivateKeyPathAsync(model, cancellationToken).ConfigureAwait(false);

    if (!string.IsNullOrEmpty(certPath))
    {
      args.Add($"--cert={certPath}");
    }

    if (!string.IsNullOrEmpty(keyPath))
    {
      args.Add($"--key={keyPath}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Always add --output=yaml to get YAML output and --dry-run=client to avoid actually creating the resource
    args.Add("--output=yaml");
    args.Add("--dry-run=client");

    return args.AsReadOnly();
  }

  /// <summary>
  /// Gets the certificate path, either from the provided path or by creating a temporary file from content.
  /// </summary>
  /// <param name="model">The TLS secret model.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The certificate file path.</returns>
  static async Task<string> GetCertificatePathAsync(TLSSecret model, CancellationToken cancellationToken = default)
  {
    if (!string.IsNullOrEmpty(model.CertificatePath) && File.Exists(model.CertificatePath))
    {
      return model.CertificatePath;
    }

    if (!string.IsNullOrEmpty(model.CertificateContent))
    {
      string tempPath = Path.Combine(Path.GetTempPath(), $"tls-cert-{Guid.NewGuid()}.crt");
      await File.WriteAllTextAsync(tempPath, model.CertificateContent, cancellationToken).ConfigureAwait(false);
      return tempPath;
    }

    throw new KubernetesGeneratorException("Either CertificatePath or CertificateContent must be provided.");
  }

  /// <summary>
  /// Gets the private key path, either from the provided path or by creating a temporary file from content.
  /// </summary>
  /// <param name="model">The TLS secret model.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The private key file path.</returns>
  static async Task<string> GetPrivateKeyPathAsync(TLSSecret model, CancellationToken cancellationToken = default)
  {
    if (!string.IsNullOrEmpty(model.PrivateKeyPath) && File.Exists(model.PrivateKeyPath))
    {
      return model.PrivateKeyPath;
    }

    if (!string.IsNullOrEmpty(model.PrivateKeyContent))
    {
      string tempPath = Path.Combine(Path.GetTempPath(), $"tls-key-{Guid.NewGuid()}.key");
      await File.WriteAllTextAsync(tempPath, model.PrivateKeyContent, cancellationToken).ConfigureAwait(false);
      return tempPath;
    }

    throw new KubernetesGeneratorException("Either PrivateKeyPath or PrivateKeyContent must be provided.");
  }
}