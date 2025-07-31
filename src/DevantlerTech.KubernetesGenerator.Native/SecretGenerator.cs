using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// Base class for secret generators.
/// </summary>
/// <typeparam name="T">The secret model type that inherits from NativeSecret.</typeparam>
public abstract class SecretGenerator<T> : NativeGenerator<T> where T : NativeSecret
{
  /// <summary>
  /// Gets the command prefix for the specific secret type (e.g., ["create", "secret", "tls"]).
  /// </summary>
  protected abstract ReadOnlyCollection<string> CommandPrefix { get; }

  /// <summary>
  /// Generates a secret using the appropriate kubectl create secret command.
  /// </summary>
  /// <param name="model">The secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(T model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var commonArgs = BuildCommonArguments(model);
    var specificArgs = await BuildSpecificArgumentsAsync(model, cancellationToken).ConfigureAwait(false);

    var args = new ReadOnlyCollection<string>(
      [.. CommandPrefix, .. commonArgs, .. specificArgs]
    );
    string errorMessage = $"Failed to create {GetSecretTypeName()} secret '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the common arguments that all secret types share (name and namespace).
  /// </summary>
  /// <param name="model">The secret model.</param>
  /// <returns>The common arguments.</returns>
  static ReadOnlyCollection<string> BuildCommonArguments(T model)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new List<string>
    {
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    return args.AsReadOnly();
  }

  /// <summary>
  /// Builds the specific arguments for the secret type.
  /// </summary>
  /// <param name="model">The secret model.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The specific arguments for this secret type.</returns>
  protected abstract Task<ReadOnlyCollection<string>> BuildSpecificArgumentsAsync(T model, CancellationToken cancellationToken = default);

  /// <summary>
  /// Gets the human-readable name of the secret type for error messages.
  /// </summary>
  /// <returns>The secret type name.</returns>
  protected virtual string GetSecretTypeName() =>
    typeof(T).Name.Replace("Secret", "", StringComparison.Ordinal).ToUpperInvariant();
}
