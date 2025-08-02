using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models.Job;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Job objects using 'kubectl create job' commands.
/// </summary>
public class JobGenerator : NativeGenerator<NativeJob>
{
  static readonly string[] _defaultArgs = ["create", "job"];

  /// <summary>
  /// Generates a job using kubectl create job command.
  /// </summary>
  /// <param name="model">The job object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when job name is not provided.</exception>
  public override async Task GenerateAsync(NativeJob model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create job '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a job from a Job object.
  /// </summary>
  /// <param name="model">The Job object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(NativeJob model)
  {
    var args = new List<string>
    {
      // Require that a job name is provided
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    args.Add($"--image={model.Spec.Image}");

    // Add command if specified
    if (model.Spec.Command != null && model.Spec.Command.Count > 0)
    {
      args.Add("--");
      args.AddRange(model.Spec.Command);
    }

    return args.AsReadOnly();
  }
}
