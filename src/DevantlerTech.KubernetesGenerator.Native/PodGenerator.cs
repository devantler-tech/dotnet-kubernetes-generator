using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Pod objects using 'kubectl run' commands.
/// </summary>
public class PodGenerator : BaseNativeGenerator<Pod>
{
  static readonly string[] _defaultArgs = ["run"];

  /// <summary>
  /// Generates a Pod using kubectl run command.
  /// </summary>
  /// <param name="model">The Pod object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  public override async Task GenerateAsync(Pod model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create Pod '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a Pod from a Pod object.
  /// </summary>
  /// <param name="model">The Pod object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl run has limitations compared to full Pod specification. 
  /// It supports single container pods with basic configuration.
  /// Advanced features like multiple containers, volumes, etc. are not supported by kubectl run.
  /// </remarks>
  static ReadOnlyCollection<string> AddArguments(Pod model)
  {
    List<string> args = [
      // The Pod name is always available from the metadata (required in constructor)
      model.Metadata.Name,
      // Add the required image
      $"--image={model.Image}"
    ];

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    // Add command if specified
    if (model.Command != null && model.Command.Count > 0)
    {
      args.Add("--command");
      args.Add("--");
      args.AddRange(model.Command);
    }
    // Add args if specified (only if command is not specified)
    else if (model.Args != null && model.Args.Count > 0)
    {
      args.Add("--");
      args.AddRange(model.Args);
    }

    // Add environment variables if specified
    if (model.Environment != null && model.Environment.Count > 0)
    {
      foreach (var env in model.Environment)
      {
        if (!string.IsNullOrEmpty(env.Key) && !string.IsNullOrEmpty(env.Value))
        {
          args.Add($"--env={env.Key}={env.Value}");
        }
      }
    }

    // Add port if specified
    if (model.Port.HasValue && model.Port.Value > 0)
    {
      args.Add($"--port={model.Port.Value}");
    }

    // Add restart policy if specified
    if (!string.IsNullOrEmpty(model.RestartPolicy))
    {
      args.Add($"--restart={model.RestartPolicy}");
    }

    // Add labels if specified
    if (model.Labels != null && model.Labels.Count > 0)
    {
      foreach (var label in model.Labels)
      {
        if (!string.IsNullOrEmpty(label.Key) && !string.IsNullOrEmpty(label.Value))
        {
          args.Add($"--labels={label.Key}={label.Value}");
        }
      }
    }

    return args.AsReadOnly();
  }
}
