using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes CronJob objects using 'kubectl create cronjob' commands.
/// </summary>
public class CronJobGenerator : BaseNativeGenerator<CronJob>
{
  static readonly string[] _defaultArgs = ["create", "cronjob"];

  /// <summary>
  /// Generates a cronjob using kubectl create cronjob command.
  /// </summary>
  /// <param name="model">The cronjob object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(CronJob model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("CronJob name is required and cannot be null or empty.");
    }
    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create cronjob '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cronjob from a CronJob object.
  /// </summary>
  /// <param name="model">The CronJob object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(CronJob model)
  {
    // Extract the first container or throw if none exists
    var firstContainer = model.Spec.JobTemplate.Template.Spec.Containers.FirstOrDefault() ?? throw new KubernetesGeneratorException("CronJob must have at least one container defined in JobTemplate.Template.Spec.Containers.");

    var args = new List<string>
    {
      model.Metadata.Name
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    args.Add($"--image={firstContainer.Image}");
    args.Add($"--schedule={model.Spec.Schedule}");

    // Add restart policy if specified
    var restartPolicy = model.Spec.JobTemplate.Template.Spec.RestartPolicy;
    if (restartPolicy.HasValue)
    {
      args.Add($"--restart={restartPolicy.Value}");
    }

    // Add command if specified
    if (firstContainer.Command != null && firstContainer.Command.Count > 0)
    {
      args.Add("--");
      args.AddRange(firstContainer.Command);
    }

    return args.AsReadOnly();
  }
}
