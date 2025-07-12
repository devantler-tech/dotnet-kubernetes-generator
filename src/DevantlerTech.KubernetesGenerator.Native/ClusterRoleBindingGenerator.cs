using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRoleBinding objects using 'kubectl create clusterrolebinding' commands.
/// </summary>
public class ClusterRoleBindingGenerator : BaseNativeGenerator<V1ClusterRoleBinding>
{
  static readonly string[] _defaultArgs = ["create", "clusterrolebinding"];

  /// <summary>
  /// Generates a cluster role binding using kubectl create clusterrolebinding command.
  /// </summary>
  /// <param name="model">The cluster role binding object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required fields are missing.</exception>
  public override async Task GenerateAsync(V1ClusterRoleBinding model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddOptions(model)]
    );
    string errorMessage = $"Failed to create cluster role binding '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cluster role binding from a V1ClusterRoleBinding object.
  /// </summary>
  /// <param name="model">The V1ClusterRoleBinding object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required fields are missing.</exception>
  static ReadOnlyCollection<string> AddOptions(V1ClusterRoleBinding model)
  {
    var args = new List<string>();

    // Require that a cluster role binding name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the cluster role binding name.");
    }
    args.Add(model.Metadata.Name);

    // Require that a cluster role is specified
    if (string.IsNullOrEmpty(model.RoleRef?.Name))
    {
      throw new KubernetesGeneratorException("The model.RoleRef.Name must be set to specify the cluster role.");
    }
    args.Add($"--clusterrole={model.RoleRef.Name}");

    // Add subjects (users, groups, service accounts)
    if (model.Subjects?.Count > 0)
    {
      foreach (var subject in model.Subjects)
      {
        switch (subject.Kind?.ToUpperInvariant())
        {
          case "USER":
            if (!string.IsNullOrEmpty(subject.Name))
            {
              args.Add($"--user={subject.Name}");
            }
            break;
          case "GROUP":
            if (!string.IsNullOrEmpty(subject.Name))
            {
              args.Add($"--group={subject.Name}");
            }
            break;
          case "SERVICEACCOUNT":
            if (!string.IsNullOrEmpty(subject.Name))
            {
              string serviceAccountRef = !string.IsNullOrEmpty(subject.NamespaceProperty)
                ? $"{subject.NamespaceProperty}:{subject.Name}"
                : $"default:{subject.Name}";
              args.Add($"--serviceaccount={serviceAccountRef}");
            }
            break;
          default:
            // Ignore unknown subject kinds
            break;
        }
      }
    }

    return args.AsReadOnly();
  }
}
