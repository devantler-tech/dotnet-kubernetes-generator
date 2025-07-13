using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ServiceAccount objects using 'kubectl create serviceaccount' commands.
/// </summary>
public class ServiceAccountGenerator : BaseNativeGenerator<ServiceAccount>
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
  public override async Task GenerateAsync(ServiceAccount model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create service account '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a service account from a ServiceAccount object.
  /// </summary>
  /// <param name="model">The ServiceAccount object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create serviceaccount only supports basic service account creation with name and namespace.
  /// Advanced properties like ImagePullSecrets, AutomountServiceAccountToken, and Secrets are not supported
  /// by the kubectl create command and will be ignored.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(ServiceAccount model)
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

    return args.AsReadOnly();
  }
}
