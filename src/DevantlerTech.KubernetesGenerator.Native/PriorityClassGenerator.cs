using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PriorityClass objects using 'kubectl create priorityclass' commands.
/// </summary>
public class PriorityClassGenerator : BaseNativeGenerator<NativePriorityClass>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(NativePriorityClass model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    if (string.IsNullOrWhiteSpace(model.Metadata.Name))
    {
      throw new KubernetesGeneratorException("A non-empty PriorityClass name must be provided.");
    }

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create priority class '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a priority class from a PriorityClass object.
  /// </summary>
  /// <param name="model">The PriorityClass object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(NativePriorityClass model)
  {
    var args = new List<string>
    {
      // Require that a priority class name is provided
      model.Metadata.Name,
      // Add the value (required parameter for kubectl create priorityclass)
      $"--value={model.Value}"
    };

    // Add description if specified
    if (!string.IsNullOrEmpty(model.Description))
    {
      args.Add($"--description={model.Description}");
    }

    // Add global-default if specified and true (kubectl omits the field when false)
    if (model.GlobalDefault == true)
    {
      args.Add("--global-default=true");
    }

    // Add preemption-policy if specified
    if (model.PreemptionPolicy.HasValue)
    {
      args.Add($"--preemption-policy={model.PreemptionPolicy.Value}");
    }

    return args.AsReadOnly();
  }
}
