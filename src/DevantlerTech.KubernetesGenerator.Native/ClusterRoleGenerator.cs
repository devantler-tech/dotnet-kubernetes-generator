using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRole objects using 'kubectl create clusterrole' commands.
/// </summary>
public class ClusterRoleGenerator : BaseNativeGenerator<V1ClusterRole>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when cluster role name is not provided or when complex rules are not supported.</exception>
  public override async Task GenerateAsync(V1ClusterRole model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create cluster role '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cluster role from a V1ClusterRole object.
  /// </summary>
  /// <param name="model">The V1ClusterRole object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <remarks>
  /// Note: kubectl create clusterrole has limitations compared to full ClusterRole YAML:
  /// - Cannot specify both aggregation rules and regular rules in a single command
  /// - Only supports single rule creation per command
  /// - API groups are specified via resource.group format
  /// When both aggregation rules and regular rules are present, aggregation rules take precedence.
  /// </remarks>
  static ReadOnlyCollection<string> AddOptions(V1ClusterRole model)
  {
    var args = new List<string>();

    // Require that a cluster role name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the cluster role name.");
    }
    args.Add(model.Metadata.Name);

    // Handle aggregation rule if present (takes precedence over regular rules)
    if (model.AggregationRule?.ClusterRoleSelectors?.Count > 0)
    {
      var aggregationRules = new List<string>();
      foreach (var selector in model.AggregationRule.ClusterRoleSelectors)
      {
        if (selector.MatchLabels?.Count > 0)
        {
          foreach (var label in selector.MatchLabels)
          {
            aggregationRules.Add($"{label.Key}={label.Value}");
          }
        }
      }
      if (aggregationRules.Count > 0)
      {
        args.Add($"--aggregation-rule={string.Join(",", aggregationRules)}");
      }

      // kubectl create clusterrole doesn't allow other options with aggregation rule
      // Return early to avoid adding rules-based options
      return args.AsReadOnly();
    }

    // Handle rules - kubectl create clusterrole typically works with single rule
    if (model.Rules?.Count > 0)
    {
      var rule = model.Rules[0]; // Take the first rule as kubectl create typically handles one rule

      // Add verbs
      if (rule.Verbs?.Count > 0)
      {
        args.Add($"--verb={string.Join(",", rule.Verbs)}");
      }
      else
      {
        // kubectl create clusterrole requires at least one verb
        throw new KubernetesGeneratorException($"kubectl create clusterrole requires at least one verb to be specified in the rule. The cluster role '{model.Metadata.Name}' has a rule with no verbs.");
      }

      // Add resources with API groups
      if (rule.Resources?.Count > 0)
      {
        var resources = new List<string>();
        foreach (string resource in rule.Resources)
        {
          if (rule.ApiGroups?.Count > 0)
          {
            foreach (string apiGroup in rule.ApiGroups)
            {
              if (string.IsNullOrEmpty(apiGroup) || apiGroup == "\"\"")
              {
                // Core API group
                resources.Add(resource);
              }
              else
              {
                // Named API group
                resources.Add($"{resource}.{apiGroup}");
              }
            }
          }
          else
          {
            resources.Add(resource);
          }
        }
        if (resources.Count > 0)
        {
          args.Add($"--resource={string.Join(",", resources)}");
        }
      }

      // Add resource names
      if (rule.ResourceNames?.Count > 0)
      {
        args.Add($"--resource-name={string.Join(",", rule.ResourceNames)}");
      }

      // Add non-resource URLs
      if (rule.NonResourceURLs?.Count > 0)
      {
        args.Add($"--non-resource-url={string.Join(",", rule.NonResourceURLs)}");
      }

      // Warn about multiple rules
      if (model.Rules.Count > 1)
      {
        throw new KubernetesGeneratorException($"kubectl create clusterrole only supports single rule creation. The cluster role '{model.Metadata.Name}' has {model.Rules.Count} rules. Only the first rule will be used.");
      }
    }
    else
    {
      // kubectl create clusterrole requires at least one verb, but we have no rules
      throw new KubernetesGeneratorException($"kubectl create clusterrole requires at least one rule with verbs. The cluster role '{model.Metadata.Name}' has no rules or empty rules.");
    }

    return args.AsReadOnly();
  }
}
