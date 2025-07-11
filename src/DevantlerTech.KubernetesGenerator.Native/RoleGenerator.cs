using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;
using System.Text;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes Role objects.
/// </summary>
public class RoleGenerator : BaseKubernetesGenerator<V1Role>
{
  /// <summary>
  /// Generates a kubectl create role command from the given V1Role model.
  /// </summary>
  /// <param name="model">The V1Role model to generate the command from.</param>
  /// <returns>The kubectl create role command as a string.</returns>
  public static string GenerateKubectlCreateCommand(V1Role model)
  {
    ArgumentNullException.ThrowIfNull(model);
    ArgumentNullException.ThrowIfNull(model.Metadata);
    ArgumentException.ThrowIfNullOrWhiteSpace(model.Metadata.Name);

    var command = new StringBuilder();
    _ = command.Append("kubectl create role ");
    _ = command.Append(model.Metadata.Name);

    if (!string.IsNullOrWhiteSpace(model.Metadata.NamespaceProperty))
    {
      _ = command.Append(" --namespace=");
      _ = command.Append(model.Metadata.NamespaceProperty);
    }

    if (model.Rules != null && model.Rules.Count > 0)
    {
      // kubectl create role only supports single rule creation
      // We'll use the first rule for the command
      var rule = model.Rules[0];

      if (rule.Verbs != null && rule.Verbs.Count > 0)
      {
        foreach (string verb in rule.Verbs)
        {
          _ = command.Append(" --verb=");
          _ = command.Append(verb);
        }
      }

      if (rule.Resources != null && rule.Resources.Count > 0)
      {
        foreach (string resource in rule.Resources)
        {
          _ = command.Append(" --resource=");
          // If API group is specified, include it in the resource specification
          if (rule.ApiGroups != null && rule.ApiGroups.Count > 0 && !string.IsNullOrWhiteSpace(rule.ApiGroups[0]))
          {
            _ = command.Append(resource);
            _ = command.Append('.');
            _ = command.Append(rule.ApiGroups[0]);
          }
          else
          {
            _ = command.Append(resource);
          }
        }
      }

      if (rule.ResourceNames != null && rule.ResourceNames.Count > 0)
      {
        foreach (string resourceName in rule.ResourceNames)
        {
          _ = command.Append(" --resource-name=");
          _ = command.Append(resourceName);
        }
      }
    }

    return command.ToString();
  }
}
