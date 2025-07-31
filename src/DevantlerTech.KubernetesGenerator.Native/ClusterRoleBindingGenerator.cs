using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes ClusterRoleBinding objects using 'kubectl create clusterrolebinding' commands.
/// </summary>
public class ClusterRoleBindingGenerator : NativeGenerator<NativeClusterRoleBinding>
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
  /// <exception cref="KubernetesGeneratorException">Thrown when cluster role binding name is not provided.</exception>
  public override async Task GenerateAsync(NativeClusterRoleBinding model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create cluster role binding '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a cluster role binding from a ClusterRoleBinding object.
  /// </summary>
  /// <param name="model">The ClusterRoleBinding object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(NativeClusterRoleBinding model)
  {
    List<string> args = [];

    // Add cluster role binding name
    args.Add(model.Metadata.Name);

    // ClusterRoleBinding only supports ClusterRole references, not Role
    if (model.RoleRef.Kind != NativeRoleBindingRoleRefKind.ClusterRole)
      throw new KubernetesGeneratorException($"ClusterRoleBinding only supports ClusterRole references, but got '{model.RoleRef.Kind}'.");

    args.Add($"--clusterrole={model.RoleRef.Name}");

    // Add subjects (users, groups, service accounts)
    if (model.Subjects != null && model.Subjects.Count > 0)
    {
      foreach (var subject in model.Subjects)
      {
        switch (subject.Kind)
        {
          case NativeRoleBindingSubjectKind.User:
            args.Add($"--user={subject.Name}");
            break;
          case NativeRoleBindingSubjectKind.Group:
            args.Add($"--group={subject.Name}");
            break;
          case NativeRoleBindingSubjectKind.ServiceAccount:
            string serviceAccountRef = !string.IsNullOrEmpty(subject.Namespace)
              ? $"{subject.Namespace}:{subject.Name}"
              : $"default:{subject.Name}";  // Default to 'default' namespace if not specified
            args.Add($"--serviceaccount={serviceAccountRef}");
            break;
          default:
            throw new KubernetesGeneratorException($"Unsupported subject kind '{subject.Kind}'.");
        }
      }
    }

    return args.AsReadOnly();
  }
}
