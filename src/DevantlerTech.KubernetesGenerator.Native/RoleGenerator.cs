using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Role objects using 'kubectl create role' commands.
/// </summary>
public class RoleGenerator : BaseNativeGenerator<V1Role>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when role name is not provided or when role rules are invalid.</exception>
  public override async Task GenerateAsync(V1Role model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create role '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a role from a V1Role object.
  /// </summary>
  /// <param name="model">The V1Role object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create role only supports creating roles with a single rule that combines all verbs and resources.
  /// Multiple rules in the V1Role object will be merged into a single rule.
  /// Advanced properties like ApiGroups and NonResourceURLs are supported where possible.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1Role model)
  {
    var args = new List<string> { };

    // Require that a role name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the role name.");
    }
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata?.NamespaceProperty))
    {
      args.Add($"--namespace={model.Metadata.NamespaceProperty}");
    }

    // Process rules - kubectl create role only supports one rule, so we need to combine all rules
    if (model.Rules != null && model.Rules.Count > 0)
    {
      var allVerbs = new HashSet<string> { };
      var allResources = new HashSet<string> { };
      var allResourceNames = new HashSet<string> { };

      foreach (var rule in model.Rules)
      {
        // Collect all verbs
        if (rule.Verbs != null)
        {
          foreach (string verb in rule.Verbs)
          {
            if (!string.IsNullOrEmpty(verb))
            {
              _ = allVerbs.Add(verb);
            }
          }
        }

        // Collect all resources, handling ApiGroups
        if (rule.Resources != null)
        {
          foreach (string resource in rule.Resources)
          {
            if (!string.IsNullOrEmpty(resource))
            {
              // Handle ApiGroups - kubectl expects format like "resource.group"
              if (rule.ApiGroups != null && rule.ApiGroups.Count > 0)
              {
                foreach (string apiGroup in rule.ApiGroups)
                {
                  if (!string.IsNullOrEmpty(apiGroup) && apiGroup.Length > 0)
                  {
                    _ = allResources.Add($"{resource}.{apiGroup}");
                  }
                  else
                  {
                    // Empty string means core API group
                    _ = allResources.Add(resource);
                  }
                }
              }
              else
              {
                _ = allResources.Add(resource);
              }
            }
          }
        }

        // Collect all resource names
        if (rule.ResourceNames != null)
        {
          foreach (string resourceName in rule.ResourceNames)
          {
            if (!string.IsNullOrEmpty(resourceName))
            {
              _ = allResourceNames.Add(resourceName);
            }
          }
        }
      }

      // Add verbs
      if (allVerbs.Count > 0)
      {
        foreach (string verb in allVerbs)
        {
          args.Add($"--verb={verb}");
        }
      }
      else
      {
        throw new KubernetesGeneratorException("At least one verb must be specified in the role rules.");
      }

      // Add resources
      if (allResources.Count > 0)
      {
        foreach (string resource in allResources)
        {
          args.Add($"--resource={resource}");
        }
      }
      else
      {
        throw new KubernetesGeneratorException("At least one resource must be specified in the role rules.");
      }

      // Add resource names
      foreach (string resourceName in allResourceNames)
      {
        args.Add($"--resource-name={resourceName}");
      }
    }
    else
    {
      throw new KubernetesGeneratorException("At least one rule must be specified in the role.");
    }

    return args.AsReadOnly();
  }
}
