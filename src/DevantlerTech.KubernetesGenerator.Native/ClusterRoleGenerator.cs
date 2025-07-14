using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRole objects using 'kubectl create clusterrole' commands.
/// </summary>
public class ClusterRoleGenerator : BaseNativeGenerator<ClusterRole>
{
  static readonly string[] _defaultArgs = ["create", "clusterrole"];

  /// <summary>
  /// Generates a cluster role using kubectl create clusterrole command.
  /// </summary>
  /// <param name="model">The cluster role object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when cluster role name is not provided.</exception>
  public override async Task GenerateAsync(ClusterRole model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create cluster role '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cluster role from a ClusterRole object.
  /// </summary>
  /// <param name="model">The ClusterRole object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(ClusterRole model)
  {
    List<string> args = [];

    // Add cluster role name - now guaranteed to be set by primary constructor
    args.Add(model.Metadata.Name);

    // Add aggregation rule if specified
    if (!string.IsNullOrEmpty(model.AggregationRule))
    {
      args.Add($"--aggregation-rule={model.AggregationRule}");
    }
    else
    {
      // Add verbs if specified
      if (model.Verbs != null && model.Verbs.Count > 0)
      {
        args.Add($"--verb={string.Join(",", model.Verbs)}");
      }

      // Add resources if specified
      if (model.Resources != null && model.Resources.Count > 0)
      {
        args.Add($"--resource={string.Join(",", model.Resources)}");
      }

      // Add resource names if specified
      if (model.ResourceNames != null && model.ResourceNames.Count > 0)
      {
        foreach (string resourceName in model.ResourceNames)
        {
          args.Add($"--resource-name={resourceName}");
        }
      }

      // Add non-resource URLs if specified
      if (model.NonResourceUrls != null && model.NonResourceUrls.Count > 0)
      {
        foreach (string nonResourceUrl in model.NonResourceUrls)
        {
          args.Add($"--non-resource-url={nonResourceUrl}");
        }
      }
    }

    return args.AsReadOnly();
  }
}
