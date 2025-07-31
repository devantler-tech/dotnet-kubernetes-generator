
namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ResourceQuota objects using 'kubectl create quota' commands.
/// </summary>
public class ResourceQuotaGenerator : BaseNativeGenerator<ResourceQuota>
{
  static readonly string[] _defaultArgs = ["create", "quota"];

  /// <summary>
  /// Generates a resource quota using kubectl create quota command.
  /// </summary>
  /// <param name="model">The resource quota object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  public override async Task GenerateAsync(ResourceQuota model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new System.Collections.ObjectModel.ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create resource quota '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a resource quota from a ResourceQuota object.
  /// </summary>
  /// <param name="model">The ResourceQuota object.</param>
  /// <returns>The kubectl arguments.</returns>
  static System.Collections.ObjectModel.ReadOnlyCollection<string> AddOptions(ResourceQuota model)
  {
    List<string> args = [];

    // Add the resource quota name (required)
    args.Add(model.Metadata.Name);

    // Add hard limits if specified
    if (model.Hard?.Count > 0)
    {
      var hardLimits = model.Hard.Select(kvp => $"{kvp.Key}={kvp.Value}");
      args.Add($"--hard={string.Join(",", hardLimits)}");
    }

    // Add scopes if specified
    if (model.Scopes?.Count > 0)
    {
      args.Add($"--scopes={string.Join(",", model.Scopes)}");
    }

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      args.Add($"--namespace={model.Metadata.Namespace}");
    }

    return args.AsReadOnly();
  }
}
