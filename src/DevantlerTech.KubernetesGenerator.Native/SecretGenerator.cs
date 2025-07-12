using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Secret objects using kubectl create secret commands.
/// </summary>
public class SecretGenerator : BaseNativeGenerator<SecretCreateOptions>
{
  /// <summary>
  /// Generates a secret using kubectl create secret command.
  /// </summary>
  /// <param name="model">The secret creation options.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="InvalidOperationException">Thrown when secret type is not supported.</exception>
  public override async Task GenerateAsync(SecretCreateOptions model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var arguments = BuildKubectlArguments(model);
    string errorMessage = $"Failed to create secret '{model.BaseOptions.Name}' using kubectl";

    await RunKubectlAsync(outputPath, overwrite, arguments, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a secret.
  /// </summary>
  /// <param name="model">The secret creation options.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> BuildKubectlArguments(SecretCreateOptions model)
  {
    var args = new List<string>
        {
            "create",
            "secret",
            model.SecretType,
            model.BaseOptions.Name
        };

    // Add type-specific arguments
    switch (model.SecretType)
    {
      case "generic":
        AddGenericSecretArguments(args, model.Generic!);
        break;
      case "docker-registry":
        AddDockerRegistrySecretArguments(args, model.DockerRegistry!);
        break;
      case "tls":
        AddTlsSecretArguments(args, model.Tls!);
        break;
      default:
        break;
    }

    // Add common arguments
    AddCommonArguments(args, model.BaseOptions);

    return args.AsReadOnly();
  }

  /// <summary>
  /// Adds generic secret arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="options">The generic secret options.</param>
  static void AddGenericSecretArguments(List<string> args, GenericSecretOptions options)
  {
    if (!string.IsNullOrEmpty(options.Type))
    {
      args.Add($"--type={options.Type}");
    }

    foreach (string file in options.FromFiles)
    {
      args.Add($"--from-file={file}");
    }

    foreach (var literal in options.FromLiterals)
    {
      args.Add($"--from-literal={literal.Key}={literal.Value}");
    }

    foreach (string envFile in options.FromEnvFiles)
    {
      args.Add($"--from-env-file={envFile}");
    }
  }

  /// <summary>
  /// Adds docker registry secret arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="options">The docker registry secret options.</param>
  static void AddDockerRegistrySecretArguments(List<string> args, DockerRegistrySecretOptions options)
  {
    if (options.FromFiles.Count > 0)
    {
      foreach (string file in options.FromFiles)
      {
        args.Add($"--from-file={file}");
      }
    }
    else
    {
      if (!string.IsNullOrEmpty(options.DockerServer))
      {
        args.Add($"--docker-server={options.DockerServer}");
      }

      if (!string.IsNullOrEmpty(options.DockerUsername))
      {
        args.Add($"--docker-username={options.DockerUsername}");
      }

      if (!string.IsNullOrEmpty(options.DockerPassword))
      {
        args.Add($"--docker-password={options.DockerPassword}");
      }

      if (!string.IsNullOrEmpty(options.DockerEmail))
      {
        args.Add($"--docker-email={options.DockerEmail}");
      }
    }
  }

  /// <summary>
  /// Adds TLS secret arguments.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="options">The TLS secret options.</param>
  static void AddTlsSecretArguments(List<string> args, TlsSecretOptions options)
  {
    args.Add($"--cert={options.CertPath}");
    args.Add($"--key={options.KeyPath}");
  }

  /// <summary>
  /// Adds common arguments for all secret types.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="options">The base secret options.</param>
  static void AddCommonArguments(List<string> args, SecretOptions options)
  {
    if (!string.IsNullOrEmpty(options.Namespace))
    {
      args.Add($"--namespace={options.Namespace}");
    }

    if (options.AppendHash)
    {
      args.Add("--append-hash");
    }

    if (!string.IsNullOrEmpty(options.Output))
    {
      args.Add($"--output={options.Output}");
    }

    if (!string.IsNullOrEmpty(options.DryRun))
    {
      args.Add($"--dry-run={options.DryRun}");
    }

    if (options.SaveConfig)
    {
      args.Add("--save-config");
    }

    if (!string.IsNullOrEmpty(options.Validate))
    {
      args.Add($"--validate={options.Validate}");
    }

    if (!string.IsNullOrEmpty(options.Template))
    {
      args.Add($"--template={options.Template}");
    }

    if (options.ShowManagedFields)
    {
      args.Add("--show-managed-fields");
    }

    if (!string.IsNullOrEmpty(options.FieldManager))
    {
      args.Add($"--field-manager={options.FieldManager}");
    }

    if (!options.AllowMissingTemplateKeys)
    {
      args.Add("--allow-missing-template-keys=false");
    }

    // Always add --output=yaml to get YAML output and --dry-run=client to avoid actually creating the resource
    if (string.IsNullOrEmpty(options.Output))
    {
      args.Add("--output=yaml");
    }

    if (string.IsNullOrEmpty(options.DryRun))
    {
      args.Add("--dry-run=client");
    }
  }
}
