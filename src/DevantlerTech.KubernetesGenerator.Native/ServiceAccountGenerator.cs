using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ServiceAccount objects using 'kubectl create serviceaccount' commands.
/// </summary>
public class ServiceAccountGenerator : BaseNativeGenerator<V1ServiceAccount>
{
  static readonly string[] _defaultArgs = ["create", "serviceaccount"];

  /// <summary>
  /// Generates a service account using kubectl create serviceaccount command.
  /// </summary>
  /// <param name="model">The service account object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when service account name is not provided.</exception>
  public override async Task GenerateAsync(V1ServiceAccount model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create service account '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a service account from a V1ServiceAccount object.
  /// </summary>
  /// <param name="model">The V1ServiceAccount object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create serviceaccount only supports basic service account creation with name and namespace.
  /// Advanced properties like ImagePullSecrets, AutomountServiceAccountToken, and Secrets are not supported
  /// by the kubectl create command and will be ignored.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1ServiceAccount model)
  {
    var args = new List<string> { };

    // Require that a service account name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the service account name.");
    }
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
