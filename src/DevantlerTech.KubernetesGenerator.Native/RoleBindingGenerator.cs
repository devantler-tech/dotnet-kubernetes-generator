using DevantlerTech.Commons.Extensions;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes RoleBinding objects.
/// </summary>
public class RoleBindingGenerator : BaseKubectlGenerator<V1RoleBinding>
{
  /// <summary>
  /// Generates a Kubernetes RoleBinding object using kubectl create.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="KubernetesGeneratorException"></exception>
  public override async Task GenerateAsync(V1RoleBinding model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model, nameof(model));
    ArgumentNullException.ThrowIfNull(model.Metadata, nameof(model.Metadata));
    ArgumentException.ThrowIfNullOrEmpty(model.Metadata.Name, nameof(model.Metadata.Name));
    
    var arguments = new List<string>
    {
      "create",
      "rolebinding",
      model.Metadata.Name,
      "--dry-run=client",
      "--output=yaml"
    };

    // Add namespace if specified
    if (!string.IsNullOrEmpty(model.Metadata.NamespaceProperty))
      arguments.Add($"--namespace={model.Metadata.NamespaceProperty}");

    // Add role reference
    if (model.RoleRef != null && !string.IsNullOrEmpty(model.RoleRef.Name))
    {
      if (model.RoleRef.Kind == "ClusterRole")
      {
        arguments.Add($"--clusterrole={model.RoleRef.Name}");
      }
      else if (model.RoleRef.Kind == "Role")
      {
        arguments.Add($"--role={model.RoleRef.Name}");
      }
    }

    // Add subjects
    if (model.Subjects != null)
    {
      foreach (var subject in model.Subjects)
      {
        switch (subject.Kind)
        {
          case "User":
            if (!string.IsNullOrEmpty(subject.Name))
              arguments.Add($"--user={subject.Name}");
            break;
          case "Group":
            if (!string.IsNullOrEmpty(subject.Name))
              arguments.Add($"--group={subject.Name}");
            break;
          case "ServiceAccount":
            if (!string.IsNullOrEmpty(subject.Name))
            {
              var serviceAccountRef = subject.NamespaceProperty != null 
                ? $"{subject.NamespaceProperty}:{subject.Name}"
                : subject.Name;
              arguments.Add($"--serviceaccount={serviceAccountRef}");
            }
            break;
        }
      }
    }

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate RoleBinding object", cancellationToken).ConfigureAwait(false);
  }
}
