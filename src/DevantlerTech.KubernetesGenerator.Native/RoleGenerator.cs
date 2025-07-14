using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Role objects using 'kubectl create role' commands.
/// </summary>
public class RoleGenerator : BaseNativeGenerator<Role>
{
  static readonly string[] _defaultArgs = ["create", "role"];

  /// <summary>
  /// Generates a role using kubectl create role command.
  /// </summary>
  /// <param name="model">The role object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when role name is not provided.</exception>
  public override async Task GenerateAsync(Role model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create role '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a role from a Role object.
  /// </summary>
  /// <param name="model">The Role object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(Role model)
  {
    List<string> args = [];

    // Add role name - now guaranteed to be set by primary constructor
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
      args.Add($"--namespace={model.Metadata.Namespace}");

    // Add rules
    if (model.Rules == null || model.Rules.Count == 0)
      throw new KubernetesGeneratorException("The role must have at least one rule.");

    foreach (var rule in model.Rules)
    {
      // Validate that the rule has required properties
      if (rule.Verbs == null || rule.Verbs.Count == 0)
        throw new KubernetesGeneratorException("Each rule must have at least one verb.");

      if (rule.Resources == null || rule.Resources.Count == 0)
        throw new KubernetesGeneratorException("Each rule must have at least one resource.");

      // Add verbs
      foreach (var verb in rule.Verbs)
        args.Add($"--verb={verb}");

      // Add resources with API groups if specified
      if (rule.ApiGroups != null && rule.ApiGroups.Count > 0)
      {
        foreach (var resource in rule.Resources)
        {
          foreach (var apiGroup in rule.ApiGroups)
          {
            if (string.IsNullOrEmpty(apiGroup))
              args.Add($"--resource={resource}");
            else
              args.Add($"--resource={resource}.{apiGroup}");
          }
        }
      }
      else
      {
        // Default to core API group (empty string)
        foreach (var resource in rule.Resources)
          args.Add($"--resource={resource}");
      }

      // Add resource names if specified
      if (rule.ResourceNames != null && rule.ResourceNames.Count > 0)
      {
        foreach (var resourceName in rule.ResourceNames)
          args.Add($"--resource-name={resourceName}");
      }
    }

    return args.AsReadOnly();
  }
}
