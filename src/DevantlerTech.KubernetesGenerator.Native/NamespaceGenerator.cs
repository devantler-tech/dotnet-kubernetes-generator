namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Namespace objects using 'kubectl create namespace' commands.
/// </summary>
public class NamespaceGenerator : BaseNativeGenerator<Models.Namespace>
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
  public override async Task GenerateAsync(Models.Namespace model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new System.Collections.ObjectModel.ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create namespace '{model.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a namespace from a Namespace object.
  /// </summary>
  /// <param name="model">The Namespace object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create namespace only supports basic namespace creation with name.
  /// Advanced properties like finalizers, labels, and annotations are not supported
  /// by the kubectl create command and will be ignored.
  /// </remarks>
  static System.Collections.ObjectModel.ReadOnlyCollection<string> AddOptions(Models.Namespace model)
  {
    List<string> args = [];

    // The name is required by the model definition
    args.Add(model.Name);

    // Add namespace option if specified in metadata
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
