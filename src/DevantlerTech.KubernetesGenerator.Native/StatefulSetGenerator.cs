using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes StatefulSet objects using 'kubectl create deployment' commands.
/// </summary>
/// <remarks>
/// Since kubectl does not have a direct 'create statefulset' command, this generator uses
/// 'kubectl create deployment' to generate the base structure and then transforms it to a StatefulSet.
/// </remarks>
public class StatefulSetGenerator : BaseNativeGenerator<V1StatefulSet>
{
  static readonly string[] _defaultArgs = ["create", "deployment"];

  /// <summary>
  /// Generates a StatefulSet using kubectl create deployment command and transforms the output.
  /// </summary>
  /// <param name="model">The StatefulSet object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when StatefulSet name is not provided.</exception>
  public override async Task GenerateAsync(V1StatefulSet model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );

    string errorMessage = $"Failed to create StatefulSet '{model.Metadata?.Name}' using kubectl";

    // Run kubectl deployment command and transform the output
    await RunKubectlAsyncWithTransformation(model, outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Runs kubectl and transforms the deployment output to StatefulSet format.
  /// </summary>
  static async Task RunKubectlAsyncWithTransformation(V1StatefulSet model, string outputPath, bool overwrite, ReadOnlyCollection<string> arguments, string errorMessage, CancellationToken cancellationToken)
  {
    // Add default arguments for YAML output and dry-run
    string[] allArguments = [.. arguments, .. new[] { "--output=yaml", "--dry-run=client" }];

    var (exitCode, output) = await DevantlerTech.KubectlCLI.Kubectl.RunAsync(allArguments, silent: true, cancellationToken: cancellationToken).ConfigureAwait(false);
    if (exitCode != 0)
      throw new KubernetesGeneratorException($"{errorMessage}: {output}");

    // Transform the deployment YAML to StatefulSet
    string statefulSetYaml = TransformDeploymentToStatefulSet(output, model);

    await YamlFileWriter.WriteToFileAsync(outputPath, statefulSetYaml, overwrite, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Transforms deployment YAML to StatefulSet YAML.
  /// </summary>
  static string TransformDeploymentToStatefulSet(string deploymentYaml, V1StatefulSet model)
  {
    // Start with the basic transformation
    string result = deploymentYaml;

    // Replace kind: Deployment with kind: StatefulSet
    result = result.Replace("kind: Deployment", "kind: StatefulSet", StringComparison.Ordinal);

    // Remove the metadata labels section
    result = result.Replace("  labels:\n    app: " + model.Metadata?.Name + "\n", "", StringComparison.Ordinal);

    // Remove strategy section
    result = result.Replace("  strategy: {}\n", "", StringComparison.Ordinal);

    // Add serviceName after selector section
    string serviceName = model.Spec?.ServiceName ?? model.Metadata?.Name ?? "statefulset";
    result = result.Replace("    matchLabels:\n      app: " + model.Metadata?.Name + "\n",
                           "    matchLabels:\n      app: " + model.Metadata?.Name + "\n  serviceName: " + serviceName + "\n",
                           StringComparison.Ordinal);

    // Remove creationTimestamp lines
    result = result.Replace("  creationTimestamp: null\n", "", StringComparison.Ordinal);
    result = result.Replace("    creationTimestamp: null\n", "", StringComparison.Ordinal);

    // Fix indentation of labels in template metadata
    result = result.Replace("      labels:\n        app: " + model.Metadata?.Name,
                           "      labels:\n        app: " + model.Metadata?.Name, StringComparison.Ordinal);
    // Also try to fix any extra indentation
    result = result.Replace("          labels:\n        app: " + model.Metadata?.Name,
                           "      labels:\n        app: " + model.Metadata?.Name, StringComparison.Ordinal);

    // Replace container name
    if (model.Spec?.Template?.Spec?.Containers?.FirstOrDefault()?.Name is string containerName)
    {
      result = result.Replace("        name: nginx", $"        name: {containerName}", StringComparison.Ordinal);
    }

    // Add command if specified
    var container = model.Spec?.Template?.Spec?.Containers?.FirstOrDefault();
    if (container?.Command != null && container.Command.Count > 0)
    {
      string commandYaml = string.Join("\n        - ", container.Command);
      result = result.Replace("      - image: nginx", $"      - command:\n        - {commandYaml}\n        image: nginx", StringComparison.Ordinal);
    }

    // Remove resources: {} and status: {} sections
    result = result.Replace("        resources: {}\n", "", StringComparison.Ordinal);
    result = result.Replace("status: {}\n", "", StringComparison.Ordinal);

    return result.TrimEnd();
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a StatefulSet from a V1StatefulSet object.
  /// </summary>
  /// <param name="model">The V1StatefulSet object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Uses kubectl create deployment as base since there's no direct kubectl create statefulset command.
  /// The output will be transformed from Deployment to StatefulSet structure.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1StatefulSet model)
  {
    var args = new List<string>();

    // Require that a StatefulSet name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the StatefulSet name.");
    }
    args.Add(model.Metadata.Name);

    // Add image if the first container has one
    string? image = model.Spec?.Template?.Spec?.Containers?.FirstOrDefault()?.Image;
    if (!string.IsNullOrEmpty(image))
    {
      args.Add($"--image={image}");
    }
    else
    {
      // Default image if none specified
      args.Add("--image=nginx");
    }

    // Add replicas if specified
    if (model.Spec?.Replicas.HasValue == true)
    {
      args.Add($"--replicas={model.Spec.Replicas.Value}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
