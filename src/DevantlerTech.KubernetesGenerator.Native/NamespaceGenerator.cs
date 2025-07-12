using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Namespace objects using 'kubectl create namespace' commands.
/// </summary>
public class NamespaceGenerator : BaseNativeGenerator<V1Namespace>
{
  static readonly string[] _defaultArgs = ["create", "namespace"];

  /// <summary>
  /// Generates a namespace using kubectl create namespace command.
  /// </summary>
  /// <param name="model">The namespace object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when namespace name is not provided.</exception>
  public override async Task GenerateAsync(V1Namespace model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create namespace '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a namespace from a V1Namespace object.
  /// </summary>
  /// <param name="model">The V1Namespace object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create namespace only supports basic namespace creation with name.
  /// Advanced properties like finalizers, labels, and annotations are not supported
  /// by the kubectl create command and will be ignored.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Namespace model)
  {
    var args = new List<string>();

    // Require that a namespace name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the namespace name.");
    }
    args.Add(model.Metadata.Name);

    return args.AsReadOnly();
  }
}
