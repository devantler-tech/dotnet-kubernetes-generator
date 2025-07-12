using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes HorizontalPodAutoscaler objects using 'kubectl autoscale' commands.
/// </summary>
public class HorizontalPodAutoscalerGenerator : BaseNativeGenerator<HorizontalPodAutoscalerCreateOptions>
{
  static readonly string[] _defaultArgs = ["autoscale"];

  /// <summary>
  /// Generates a HorizontalPodAutoscaler using kubectl autoscale command.
  /// </summary>
  /// <param name="model">The HorizontalPodAutoscaler creation options.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required fields are missing.</exception>
  public override async Task GenerateAsync(HorizontalPodAutoscalerCreateOptions model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create HorizontalPodAutoscaler '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a HorizontalPodAutoscaler from the options.
  /// </summary>
  /// <param name="model">The HorizontalPodAutoscaler creation options.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required fields are missing.</exception>
  static ReadOnlyCollection<string> AddOptions(HorizontalPodAutoscalerCreateOptions model)
  {
    var args = new List<string>();

    // Validate required fields
    if (model.ScaleTargetRef == null)
    {
      throw new KubernetesGeneratorException("The model.ScaleTargetRef must be set to specify the target resource.");
    }

    if (string.IsNullOrEmpty(model.ScaleTargetRef.Kind))
    {
      throw new KubernetesGeneratorException("The model.ScaleTargetRef.Kind must be set to specify the target resource type.");
    }

    if (string.IsNullOrEmpty(model.ScaleTargetRef.Name))
    {
      throw new KubernetesGeneratorException("The model.ScaleTargetRef.Name must be set to specify the target resource name.");
    }

    // Add resource type and name
    args.Add($"{model.ScaleTargetRef.Kind.ToUpperInvariant()}/{model.ScaleTargetRef.Name}");

    // Add required max replicas
    args.Add($"--max={model.MaxReplicas}");

    // Add optional min replicas
    if (model.MinReplicas.HasValue)
    {
      args.Add($"--min={model.MinReplicas.Value}");
    }

    // Add optional CPU percentage
    if (model.TargetCPUUtilizationPercentage.HasValue)
    {
      args.Add($"--cpu-percent={model.TargetCPUUtilizationPercentage.Value}");
    }

    // Add optional name (defaults to resource name if not specified)
    if (!string.IsNullOrEmpty(model.Metadata?.Name))
    {
      args.Add($"--name={model.Metadata.Name}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    return args.AsReadOnly();
  }
}
