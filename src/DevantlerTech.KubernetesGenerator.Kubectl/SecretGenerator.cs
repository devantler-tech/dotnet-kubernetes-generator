using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Kubectl.Models;

namespace DevantlerTech.KubernetesGenerator.Kubectl;

/// <summary>
/// Generator for creating Kubernetes secrets using kubectl create command.
/// </summary>
public class SecretGenerator : BaseKubectlGenerator<KubectlSecretBase>
{
  /// <summary>
  /// Generates a Kubernetes secret using kubectl create command.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public override async Task GenerateAsync(KubectlSecretBase model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));

    var arguments = new List<string> { "create", "secret" };

    switch (model)
    {
      case KubectlGenericSecret genericSecret:
        await GenerateGenericSecretAsync(genericSecret, arguments, outputPath, overwrite, cancellationToken).ConfigureAwait(false);
        break;
      case KubectlDockerRegistrySecret dockerSecret:
        await GenerateDockerRegistrySecretAsync(dockerSecret, arguments, outputPath, overwrite, cancellationToken).ConfigureAwait(false);
        break;
      case KubectlTlsSecret tlsSecret:
        await GenerateTlsSecretAsync(tlsSecret, arguments, outputPath, overwrite, cancellationToken).ConfigureAwait(false);
        break;
      default:
        throw new NotSupportedException($"Secret type {model.GetType().Name} is not supported.");
    }
  }

  async Task GenerateGenericSecretAsync(KubectlGenericSecret model, List<string> arguments, string outputPath, bool overwrite, CancellationToken cancellationToken)
  {
    arguments.Add("generic");
    arguments.Add(model.Name);

    // Add type if not default
    if (!string.IsNullOrEmpty(model.Type) && model.Type != "Opaque")
    {
      arguments.Add($"--type={model.Type}");
    }

    // Add from-literal values
    if (model.FromLiteral != null)
    {
      foreach (var kvp in model.FromLiteral)
      {
        arguments.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }

    // Add from-file values
    if (model.FromFile != null)
    {
      foreach (var kvp in model.FromFile)
      {
        arguments.Add($"--from-file={kvp.Key}={kvp.Value}");
      }
    }

    // Add from-env-file values
    if (model.FromEnvFile != null)
    {
      foreach (string envFile in model.FromEnvFile)
      {
        arguments.Add($"--from-env-file={envFile}");
      }
    }

    AddCommonArguments(model, arguments);

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate generic secret", cancellationToken).ConfigureAwait(false);
  }

  async Task GenerateDockerRegistrySecretAsync(KubectlDockerRegistrySecret model, List<string> arguments, string outputPath, bool overwrite, CancellationToken cancellationToken)
  {
    arguments.Add("docker-registry");
    arguments.Add(model.Name);

    if (!string.IsNullOrEmpty(model.FromFile))
    {
      arguments.Add($"--from-file={model.FromFile}");
    }
    else
    {
      arguments.Add($"--docker-server={model.DockerServer}");
      arguments.Add($"--docker-username={model.DockerUsername}");
      arguments.Add($"--docker-password={model.DockerPassword}");
      arguments.AddIfNotNull("--docker-email={0}", model.DockerEmail);
    }

    AddCommonArguments(model, arguments);

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate docker-registry secret", cancellationToken).ConfigureAwait(false);
  }

  async Task GenerateTlsSecretAsync(KubectlTlsSecret model, List<string> arguments, string outputPath, bool overwrite, CancellationToken cancellationToken)
  {
    arguments.Add("tls");
    arguments.Add(model.Name);
    arguments.Add($"--cert={model.CertPath}");
    arguments.Add($"--key={model.KeyPath}");

    AddCommonArguments(model, arguments);

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate TLS secret", cancellationToken).ConfigureAwait(false);
  }

  static void AddCommonArguments(KubectlSecretBase model, List<string> arguments)
  {
    // Add namespace
    arguments.AddIfNotNull("--namespace={0}", model.Namespace);

    // Add append hash
    if (model.AppendHash)
    {
      arguments.Add("--append-hash");
    }

    // Add dry-run and output format
    arguments.Add("--dry-run=client");
    arguments.Add("--output=yaml");
  }
}