using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Job objects using 'kubectl create job' commands.
/// </summary>
public class JobGenerator : BaseNativeGenerator<Job>
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
  public override async Task GenerateAsync(Job model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create job '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a job from a Job object.
  /// </summary>
  /// <param name="model">The Job object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddArguments(Job model)
  {
    var args = new List<string>
    {
      model.Metadata.Name,
      $"--image={model.Image}"
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add command if specified
    if (model.Command.Count > 0)
    {
      args.Add($"--command={string.Join(" ", model.Command)}");
    }

    // Add restart policy if specified
    if (!string.IsNullOrEmpty(model.RestartPolicy))
    {
      args.Add($"--restart={model.RestartPolicy}");
    }

    return args.AsReadOnly();
  }
}
