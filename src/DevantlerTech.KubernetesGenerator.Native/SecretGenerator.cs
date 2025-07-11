using System.Collections.ObjectModel;
using System.Text;
using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Secret objects using kubectl commands.
/// </summary>
public class SecretGenerator : IKubernetesGenerator<V1Secret>
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
  public async Task GenerateAsync(V1Secret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));

    var arguments = new List<string> { "create", "secret", "generic", model.Metadata.Name };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.NamespaceProperty))
    {
      arguments.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Add type if not default
    if (!string.IsNullOrEmpty(model.Type) && model.Type != "Opaque")
    {
      arguments.Add($"--type={model.Type}");
    }

    // Add string data as literals
    if (model.StringData != null)
    {
      foreach (var kvp in model.StringData)
      {
        arguments.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }

    // Add binary data as literals (base64 decode first), but only for keys not in StringData
    if (model.Data != null)
    {
      foreach (var kvp in model.Data)
      {
        // Skip if key already exists in StringData
        if (model.StringData != null && model.StringData.ContainsKey(kvp.Key))
          continue;
          
        var value = Encoding.UTF8.GetString(kvp.Value);
        arguments.Add($"--from-literal={kvp.Key}={value}");
      }
    }

    // Add dry-run and output format
    arguments.Add("--dry-run=client");
    arguments.Add("--output=yaml");

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate secret", cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Runs the kubectl CLI with the provided arguments and writes the output to the specified file.
  /// </summary>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="arguments"></param>
  /// <param name="errorMessage"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  async Task RunKubectlAsync(string outputPath, bool overwrite, ReadOnlyCollection<string> arguments, string errorMessage, CancellationToken cancellationToken)
  {
    var (exitCode, output) = await KubectlCLI.Kubectl.RunAsync([.. arguments], silent: true,
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"{errorMessage}: {output}");
    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
