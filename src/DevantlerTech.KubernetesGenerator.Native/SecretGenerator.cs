using System.Collections.ObjectModel;
using System.Text;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Secret objects using kubectl create secret commands.
/// </summary>
public class SecretGenerator : BaseNativeGenerator<V1Secret>
{
  /// <summary>
  /// Generates a secret using kubectl create secret command.
  /// </summary>
  /// <param name="model">The secret object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="InvalidOperationException">Thrown when secret type is not supported.</exception>
  public override async Task GenerateAsync(V1Secret model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    string errorMessage = $"Failed to create secret '{model.Metadata?.Name}' using kubectl";

    await RunKubectlAsync(outputPath, overwrite, AddArguments(model), errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a secret from a V1Secret object.
  /// </summary>
  /// <param name="model">The V1Secret object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(V1Secret model)
  {
    var args = new List<string> { "create", "secret" };

    // Determine secret type and add appropriate arguments
    string secretType = DetermineSecretType(model);
    args.Add(secretType);
    args.Add(model.Metadata?.Name ?? "unnamed-secret");

    // Add type-specific arguments based on secret type
    switch (secretType)
    {
      case "generic":
        AddGenericSecretArguments(args, model);
        break;
      case "docker-registry":
        AddDockerRegistrySecretArguments(args, model);
        break;
      case "tls":
        AddTlsSecretArguments(args, model);
        break;
      default:
        AddGenericSecretArguments(args, model);
        break;
    }

    // Add common arguments
    AddCommonArguments(args, model);

    return args.AsReadOnly();
  }

  /// <summary>
  /// Determines the kubectl secret type based on the V1Secret object.
  /// </summary>
  /// <param name="model">The V1Secret object.</param>
  /// <returns>The kubectl secret type.</returns>
  static string DetermineSecretType(V1Secret model)
  {
    return model.Type switch
    {
      "kubernetes.io/dockerconfigjson" => "docker-registry",
      "kubernetes.io/tls" => "tls",
      _ => "generic"
    };
  }

  /// <summary>
  /// Adds generic secret arguments from V1Secret data.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Secret object.</param>
  static void AddGenericSecretArguments(List<string> args, V1Secret model)
  {
    // Always set secret type to ensure consistent output
    string secretType = string.IsNullOrEmpty(model.Type) ? "Opaque" : model.Type;
    args.Add($"--type={secretType}");

    // Add data from StringData (takes precedence over Data)
    if (model.StringData?.Count > 0)
    {
      foreach (var kvp in model.StringData)
      {
        args.Add($"--from-literal={kvp.Key}={kvp.Value}");
      }
    }
    else if (model.Data?.Count > 0)
    {
      // Convert base64 data back to literals
      foreach (var kvp in model.Data)
      {
        string value = Encoding.UTF8.GetString(kvp.Value);
        args.Add($"--from-literal={kvp.Key}={value}");
      }
    }
  }

  /// <summary>
  /// Adds docker registry secret arguments from V1Secret data.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Secret object.</param>
  static void AddDockerRegistrySecretArguments(List<string> args, V1Secret model)
  {
    // For docker registry secrets, we need to extract the docker config data
    if (model.Data?.ContainsKey(".dockerconfigjson") == true)
    {
      // If we have .dockerconfigjson, use --from-file approach
      // Note: This is a simplified approach - in real scenarios we'd need to create a temp file
      string dockerConfigJson = Encoding.UTF8.GetString(model.Data[".dockerconfigjson"]);

      // For now, we'll use a generic approach since we can't easily extract individual fields
      // from the dockerconfigjson without parsing the JSON
      args.Add($"--from-literal=.dockerconfigjson={dockerConfigJson}");
    }
    else
    {
      // Fallback to generic approach
      AddGenericSecretArguments(args, model);
    }
  }

  /// <summary>
  /// Adds TLS secret arguments from V1Secret data.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Secret object.</param>
  static void AddTlsSecretArguments(List<string> args, V1Secret model)
  {
    // For TLS secrets, we need cert and key data
    // Note: This is a simplified approach - in real scenarios we'd need to create temp files
    if (model.Data?.ContainsKey("tls.crt") == true && model.Data?.ContainsKey("tls.key") == true)
    {
      // For now, we'll use a generic approach since kubectl create secret tls requires file paths
      AddGenericSecretArguments(args, model);
    }
    else
    {
      // Fallback to generic approach
      AddGenericSecretArguments(args, model);
    }
  }

  /// <summary>
  /// Adds common arguments for all secret types.
  /// </summary>
  /// <param name="args">The arguments list.</param>
  /// <param name="model">The V1Secret object.</param>
  static void AddCommonArguments(List<string> args, V1Secret model)
  {
    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Always add --output=yaml to get YAML output and --dry-run=client to avoid actually creating the resource
    args.Add("--output=yaml");
    args.Add("--dry-run=client");
  }
}
