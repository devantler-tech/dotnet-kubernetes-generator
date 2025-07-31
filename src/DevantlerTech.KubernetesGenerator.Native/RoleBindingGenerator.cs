using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models.RoleBinding;
using DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes RoleBinding objects using 'kubectl create rolebinding' commands.
/// </summary>
public class RoleBindingGenerator : NativeGenerator<NativeRoleBinding>
{
  static readonly string[] _defaultArgs = ["create", "rolebinding"];

  /// <summary>
  /// Generates a role binding using kubectl create rolebinding command.
  /// </summary>
  /// <param name="model">The role binding object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when role binding name is not provided.</exception>
  public override async Task GenerateAsync(NativeRoleBinding model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. AddArguments(model)]
    );
    string errorMessage = $"Failed to create role binding '{model.Metadata.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a role binding from a RoleBinding object.
  /// </summary>
  /// <param name="model">The RoleBinding object.</param>
  /// <returns>The kubectl arguments.</returns>
  /// <exception cref="KubernetesGeneratorException">Thrown when required properties are missing.</exception>
  static ReadOnlyCollection<string> AddArguments(NativeRoleBinding model)
  {
    List<string> args = [];

    // Add role binding name - guaranteed to be set as a required property
    args.Add(model.Metadata.Name);

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
      args.Add($"--namespace={model.Metadata.Namespace}");

    switch (model.RoleRef.Kind)
    {
      case NativeRoleBindingRoleRefKind.ClusterRole:
        args.Add($"--clusterrole={model.RoleRef.Name}");
        break;
      case NativeRoleBindingRoleRefKind.Role:
        args.Add($"--role={model.RoleRef.Name}");
        break;
      default:
        throw new KubernetesGeneratorException($"Unsupported RoleRef.Kind '{model.RoleRef.Kind}'.");
    }

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
              : subject.Name;
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
