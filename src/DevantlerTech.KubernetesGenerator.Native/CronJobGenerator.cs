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
  public override async Task GenerateAsync(CronJob model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create cronjob '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cronjob from a CronJob object.
  /// </summary>
  /// <param name="model">The CronJob object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(CronJob model)
  {
    var args = new List<string>
    {
      model.Metadata.Name,
      $"--schedule={model.Schedule}",
      $"--image={model.Image}"
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add command if specified
    if (model.Command?.Count > 0)
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
