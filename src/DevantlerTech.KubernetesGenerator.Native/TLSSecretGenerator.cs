using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for TLS Kubernetes Secret objects using 'kubectl create secret tls' commands.
/// </summary>
public class TLSSecretGenerator : BaseNativeGenerator<TLSSecret>
{
  static readonly string[] _defaultArgs = ["create", "secret", "tls"];
  /// <summary>
  /// List of temporary files created during generation that need to be cleaned up.
  /// </summary>
  readonly List<string> _temporaryFiles = [];

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

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. await AddArgumentsAsync(model, cancellationToken).ConfigureAwait(false)]
    );
    string errorMessage = $"Failed to create TLS secret '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);

    CleanUpTemporaryFiles();
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a TLS secret from a TLSSecret object.
  /// </summary>
  /// <param name="model">The TLSSecret object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The kubectl arguments.</returns>
  async Task<ReadOnlyCollection<string>> AddArgumentsAsync(TLSSecret model, CancellationToken cancellationToken = default)
  {
    var args = new List<string>
    {
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Handle certificate and key data
    string certPath = await GetFilePathAsync(model.Certificate, "tls-cert.crt", cancellationToken).ConfigureAwait(false);
    string keyPath = await GetFilePathAsync(model.PrivateKey, "tls-key.key", cancellationToken).ConfigureAwait(false);

    args.Add($"--cert={certPath}");
    args.Add($"--key={keyPath}");

    return args.AsReadOnly();
  }

  /// <summary>
  /// Gets the file path for certificate or key data, either from the provided path or by creating a temporary file from content.
  /// </summary>
  /// <param name="data">The certificate or key data (path or content).</param>
  /// <param name="fileName">The file name to use for temporary files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The file path.</returns>
  async Task<string> GetFilePathAsync(string data, string fileName, CancellationToken cancellationToken = default)
  {
    if (File.Exists(data))
    {
      return data;
    }

    if (!data.Contains("-----BEGIN", StringComparison.Ordinal) || !data.Contains("-----END", StringComparison.Ordinal))
    {
      throw new KubernetesGeneratorException($"The provided data for {fileName} is not a valid certificate or key content.");
    }

    // Treat the data as content and create a temporary file
    string tempPath = Path.Combine(Path.GetTempPath(), $"{fileName}-{Guid.NewGuid()}");
    await File.WriteAllTextAsync(tempPath, data, cancellationToken).ConfigureAwait(false);
    _temporaryFiles.Add(tempPath);
    return tempPath;
  }

  /// <summary>
  /// Cleans up temporary files created during the generation process.
  /// </summary>
  void CleanUpTemporaryFiles()
  {
    foreach (string tempFile in _temporaryFiles)
    {
      if (File.Exists(tempFile))
      {
        File.Delete(tempFile);
      }
    }
    _temporaryFiles.Clear();
  }
}
