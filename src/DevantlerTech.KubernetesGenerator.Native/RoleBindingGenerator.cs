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
    
    var arguments = new List<string>
    {
      "create",
      "rolebinding",
      model.Metadata.Name,
      "--dry-run=client",
      "--output=yaml"
    };

    // Add namespace if specified
    arguments.AddIfNotNull("--namespace={0}", model.Metadata.NamespaceProperty);

    // Add role reference
    if (model.RoleRef != null)
    {
      if (model.RoleRef.Kind == "ClusterRole")
      {
        arguments.AddIfNotNull("--clusterrole={0}", model.RoleRef.Name);
      }
      else if (model.RoleRef.Kind == "Role")
      {
        arguments.AddIfNotNull("--role={0}", model.RoleRef.Name);
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
            arguments.AddIfNotNull("--user={0}", subject.Name);
            break;
          case "Group":
            arguments.AddIfNotNull("--group={0}", subject.Name);
            break;
          case "ServiceAccount":
            var serviceAccountRef = subject.NamespaceProperty != null 
              ? $"{subject.NamespaceProperty}:{subject.Name}"
              : subject.Name;
            arguments.AddIfNotNull("--serviceaccount={0}", serviceAccountRef);
            break;
        }
      }
    }

    await RunKubectlAsync(outputPath, overwrite, arguments.AsReadOnly(), "Failed to generate RoleBinding object", cancellationToken).ConfigureAwait(false);
  }
}
