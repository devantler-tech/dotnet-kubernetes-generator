using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Ingress objects using 'kubectl create ingress' commands.
/// </summary>
public class IngressGenerator : NativeGenerator<NativeIngress>
{
  static readonly string[] _defaultArgs = ["create", "ingress"];

  /// <summary>
  /// Generates an ingress using kubectl create ingress command.
  /// </summary>
  /// <param name="model">The ingress object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when ingress name is not provided.</exception>
  public override async Task GenerateAsync(NativeIngress model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create ingress '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating an ingress from an Ingress object.
  /// </summary>
  /// <param name="model">The Ingress object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(NativeIngress model)
  {
    List<string> args = [];

    // Require that an ingress name is provided
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add ingress class if specified
    if (!string.IsNullOrEmpty(model.Class))
    {
      args.Add($"--class={model.Class}");
    }

    // Add default backend if specified
    if (!string.IsNullOrEmpty(model.DefaultBackend))
    {
      args.Add($"--default-backend={model.DefaultBackend}");
    }

    // Add rules
    if (model.Rules != null && model.Rules.Count > 0)
    {
      foreach (var rule in model.Rules)
      {
        args.Add($"--rule={rule.ToKubectlFormat()}");
      }
    }

    // Add annotations
    if (model.Annotations != null && model.Annotations.Count > 0)
    {
      foreach (var annotation in model.Annotations)
      {
        args.Add($"--annotation={annotation.Key}={annotation.Value}");
      }
    }

    return args.AsReadOnly();
  }
}
