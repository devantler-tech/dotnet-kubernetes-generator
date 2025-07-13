using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Namespace objects using 'kubectl create namespace' commands.
/// </summary>
public class NamespaceGenerator : BaseNativeGenerator<Namespace>
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
  public override async Task GenerateAsync(Namespace model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new System.Collections.ObjectModel.ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create namespace '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a namespace from a Namespace object.
  /// </summary>
  /// <param name="model">The Namespace object.</param>
  /// <returns>The kubectl arguments.</returns>
  static System.Collections.ObjectModel.ReadOnlyCollection<string> AddArguments(Namespace model)
  {
    var args = new List<string>
    {
      model.Metadata.Name
    };

    return args.AsReadOnly();
  }
}
