using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PriorityClass objects using 'kubectl create priorityclass' commands.
/// </summary>
public class PriorityClassGenerator : BaseNativeGenerator<V1PriorityClass>
{
  static readonly string[] _defaultArgs = ["create", "priorityclass"];

  /// <summary>
  /// Generates a priority class using kubectl create priorityclass command.
  /// </summary>
  /// <param name="model">The priority class object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when priority class name is not provided.</exception>
  public override async Task GenerateAsync(V1PriorityClass model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create priority class '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a priority class from a V1PriorityClass object.
  /// </summary>
  /// <param name="model">The V1PriorityClass object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: Priority classes are cluster-scoped resources, so namespace is not supported.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1PriorityClass model)
  {
    var args = new List<string> { };

    // Require that a priority class name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the priority class name.");
    }
    args.Add(model.Metadata.Name);

    // Add value (required)
    args.Add($"--value={model.Value}");

    // Add global default if specified
    if (model.GlobalDefault.HasValue && model.GlobalDefault.Value)
    {
      args.Add("--global-default=true");
    }

    // Add description if specified
    if (!string.IsNullOrEmpty(model.Description))
    {
      args.Add($"--description={model.Description}");
    }

    // Add preemption policy if specified
    if (!string.IsNullOrEmpty(model.PreemptionPolicy))
    {
      args.Add($"--preemption-policy={model.PreemptionPolicy}");
    }

    return args.AsReadOnly();
  }
}
