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
  /// <exception cref="KubernetesGeneratorException">Thrown when job name is not provided.</exception>
  public override async Task GenerateAsync(Job model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = AddOptions(model);

    // Check if the args contain "--" separator
    bool hasCommandSeparator = args.Contains("--");

    if (hasCommandSeparator)
    {
      // When there's a command separator, we need to manually handle the default arguments
      // to ensure they come before the "--" separator
      var argsBeforeSeparator = new List<string>();
      var argsAfterSeparator = new List<string>();
      bool afterSeparator = false;

      foreach (string arg in args)
      {
        if (arg == "--")
        {
          afterSeparator = true;
        }

        if (afterSeparator)
        {
          argsAfterSeparator.Add(arg);
        }
        else
        {
          argsBeforeSeparator.Add(arg);
        }
      }

      // Build the final arguments with default arguments in correct position
      var finalArgs = new List<string>();
      finalArgs.AddRange(_defaultArgs);
      finalArgs.AddRange(argsBeforeSeparator);
      finalArgs.AddRange(["--output=yaml", "--dry-run=client"]);
      finalArgs.AddRange(argsAfterSeparator);

      var finalArguments = new ReadOnlyCollection<string>(finalArgs);
      string errorMessage = $"Failed to create job '{model.Metadata?.Name}' using kubectl";

      // Call kubectl directly to avoid the automatic appending of default arguments
      (int exitCode, string output) = await DevantlerTech.KubectlCLI.Kubectl.RunAsync([.. finalArguments], silent: true, cancellationToken: cancellationToken).ConfigureAwait(false);
      if (exitCode != 0)
        throw new KubernetesGeneratorException($"{errorMessage}: {output}");
      await Core.YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
    }
    else
    {
      // No command separator, use the base implementation
      var arguments = new ReadOnlyCollection<string>(
        [.. _defaultArgs, .. args]
      );
      string errorMessage = $"Failed to create job '{model.Metadata?.Name}' using kubectl";
      await RunKubectlAsync(outputPath, overwrite, arguments, errorMessage, cancellationToken).ConfigureAwait(false);
    }
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a job from a Job object.
  /// </summary>
  /// <param name="model">The Job object.</param>
  /// <returns>The kubectl arguments.</returns>
  static ReadOnlyCollection<string> AddOptions(Job model)
  {
    var args = new List<string>();

    // Require that a job name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the job name.");
    }
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Add image or from parameter
    if (!string.IsNullOrEmpty(model.From))
    {
      args.Add($"--from={model.From}");
    }
    else
    {
      args.Add($"--image={model.Image}");
    }

    // Add command and args if specified
    if (model.Command?.Count > 0)
    {
      args.Add("--");
      args.AddRange(model.Command);

      // Add args if present
      if (model.Args?.Count > 0)
      {
        args.AddRange(model.Args);
      }
    }

    return args.AsReadOnly();
  }
}
