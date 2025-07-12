using System.Collections.ObjectModel;
using System.Text;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for generic Kubernetes Secret objects using 'kubectl create secret generic' commands.
/// </summary>
public class GenericSecretGenerator : BaseNativeGenerator<V1Secret>
{
  /// <summary>
  /// Generates a generic secret using kubectl create secret generic command.
  /// </summary>
  /// <param name="model">The secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when secret name is not provided.</exception>
  public override async Task GenerateAsync(V1Secret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    string errorMessage = $"Failed to create generic secret '{model.Metadata?.Name}' using kubectl";

    await RunKubectlAsync(outputPath, overwrite, AddArguments(model), errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a generic secret from a V1Secret object.
  /// </summary>
  /// <param name="model">The V1Secret object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(V1Secret model)
  {
    var args = new List<string> { "create", "secret", "generic" };
    
    // Require that a secret name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the secret name.");
    }
    args.Add(model.Metadata.Name);

    // Add type if specified (but don't require it)
    if (!string.IsNullOrEmpty(model.Type))
    {
      args.Add($"--type={model.Type}");
    }

    // Combine data from both Data and StringData, with StringData taking precedence
    var combinedData = new Dictionary<string, string>();
    
    // First add data from Data (base64 decoded)
    if (model.Data?.Count > 0)
    {
      foreach (var kvp in model.Data)
      {
        string value = Encoding.UTF8.GetString(kvp.Value);
        combinedData[kvp.Key] = value;
      }
    }
    
    // Then add/override with StringData (takes precedence)
    if (model.StringData?.Count > 0)
    {
      foreach (var kvp in model.StringData)
      {
        combinedData[kvp.Key] = kvp.Value;
      }
    }
    
    // Add all combined data as literals
    foreach (var kvp in combinedData)
    {
      args.Add($"--from-literal={kvp.Key}={kvp.Value}");
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
}