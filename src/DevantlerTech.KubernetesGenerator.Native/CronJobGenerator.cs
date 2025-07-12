using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes CronJob objects using 'kubectl create cronjob' commands.
/// </summary>
public class CronJobGenerator : BaseNativeGenerator<V1CronJob>
{
  /// <summary>
  /// Generates a CronJob using kubectl create cronjob command.
  /// </summary>
  /// <param name="model">The CronJob object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required fields are missing.</exception>
  public override async Task GenerateAsync(V1CronJob model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = AddOptions(model);

    // For commands with the -- separator, we need to handle the default arguments differently
    // to ensure they appear before the -- separator
    var containers = model.Spec?.JobTemplate?.Spec?.Template?.Spec?.Containers;
    bool hasCommand = containers?[0]?.Command?.Count > 0;

    ReadOnlyCollection<string> allArgs;
    if (hasCommand)
    {
      // Find the -- separator and insert default arguments before it
      var argsList = args.ToList();
      int separatorIndex = argsList.IndexOf("--");
      if (separatorIndex >= 0)
      {
        argsList.InsertRange(separatorIndex, ["--output=yaml", "--dry-run=client"]);
      }
      // Add the create cronjob commands at the beginning
      var combinedArgs = new List<string> { "create", "cronjob" };
      combinedArgs.AddRange(argsList);
      allArgs = combinedArgs.AsReadOnly();
    }
    else
    {
      // No command, let the base class handle default arguments
      var combinedArgs = new List<string> { "create", "cronjob" };
      combinedArgs.AddRange(args);
      combinedArgs.AddRange(["--output=yaml", "--dry-run=client"]);
      allArgs = combinedArgs.AsReadOnly();
    }

    string errorMessage = $"Failed to create CronJob '{model.Metadata?.Name}' using kubectl";

    // Call kubectl directly instead of using the base class method to avoid double-adding default args
    var (exitCode, output) = await DevantlerTech.KubectlCLI.Kubectl.RunAsync([.. allArgs], silent: true,
      cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"{errorMessage}: {output}");
    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a CronJob from a V1CronJob object.
  /// </summary>
  /// <param name="model">The V1CronJob object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create cronjob supports basic CronJob creation with name, schedule, and image.
  /// Advanced properties like complex job specifications, multiple containers, and advanced scheduling
  /// options are not fully supported by the kubectl create command and may be ignored.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1CronJob model)
  {
    var args = new List<string> { };

    // Require that a CronJob name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the CronJob name.");
    }
    args.Add(model.Metadata.Name);

    // Require that a schedule is provided
    if (string.IsNullOrEmpty(model.Spec?.Schedule))
    {
      throw new KubernetesGeneratorException("The model.Spec.Schedule must be set to set the CronJob schedule.");
    }
    args.Add($"--schedule={model.Spec.Schedule}");

    // Require that at least one container with an image is provided
    var containers = model.Spec?.JobTemplate?.Spec?.Template?.Spec?.Containers;
    if (containers?.Count == 0 || containers == null)
    {
      throw new KubernetesGeneratorException("The model.Spec.JobTemplate.Spec.Template.Spec.Containers must contain at least one container.");
    }

    var firstContainer = containers[0];
    if (string.IsNullOrEmpty(firstContainer.Image))
    {
      throw new KubernetesGeneratorException("The first container in model.Spec.JobTemplate.Spec.Template.Spec.Containers must have an image.");
    }
    args.Add($"--image={firstContainer.Image}");

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Add command and args if specified (passed after -- separator)
    if (firstContainer.Command?.Count > 0)
    {
      args.Add("--");
      args.AddRange(firstContainer.Command);
    }

    return args.AsReadOnly();
  }
}
