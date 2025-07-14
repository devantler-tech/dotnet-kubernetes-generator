using System.Collections.ObjectModel;
using System.Text;
using DevantlerTech.KubectlCLI;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PersistentVolume objects using kubectl create commands.
/// </summary>
public class PersistentVolumeGenerator : BaseNativeGenerator<PersistentVolume>
{
  static readonly string[] _defaultArgs = ["create", "-f", "-"];

  /// <summary>
  /// Generates a persistent volume using kubectl create -f command with generated YAML.
  /// </summary>
  /// <param name="model">The persistent volume object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when persistent volume name is not provided.</exception>
  public override async Task GenerateAsync(PersistentVolume model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the persistent volume name.");
    }

    // Generate YAML content for the persistent volume
    string yamlContent = GenerateYamlContent(model);

    // Use kubectl create -f with stdin to create the persistent volume
    var (exitCode, output) = await Kubectl.RunAsync(_defaultArgs, yamlContent, silent: true, cancellationToken: cancellationToken).ConfigureAwait(false);
    
    if (exitCode != 0)
    {
      throw new KubernetesGeneratorException($"Failed to create persistent volume '{model.Metadata.Name}' using kubectl: {output}");
    }

    // Write the generated YAML to the output file
    await YamlFileWriter.WriteToFileAsync(outputPath, output, overwrite, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Generates YAML content for the persistent volume.
  /// </summary>
  /// <param name="model">The persistent volume model.</param>
  /// <returns>The YAML content as a string.</returns>
  static string GenerateYamlContent(PersistentVolume model)
  {
    var yaml = new StringBuilder();
    
    yaml.AppendLine("apiVersion: v1");
    yaml.AppendLine("kind: PersistentVolume");
    yaml.AppendLine("metadata:");
    yaml.AppendLine($"  name: {model.Metadata.Name}");
    
    if (!string.IsNullOrEmpty(model.Metadata.Namespace))
    {
      yaml.AppendLine($"  namespace: {model.Metadata.Namespace}");
    }

    if (model.Metadata.Labels?.Count > 0)
    {
      yaml.AppendLine("  labels:");
      foreach (var label in model.Metadata.Labels)
      {
        yaml.AppendLine($"    {label.Key}: {label.Value}");
      }
    }

    if (model.Metadata.Annotations?.Count > 0)
    {
      yaml.AppendLine("  annotations:");
      foreach (var annotation in model.Metadata.Annotations)
      {
        yaml.AppendLine($"    {annotation.Key}: {annotation.Value}");
      }
    }

    yaml.AppendLine("spec:");

    if (model.Capacity != null)
    {
      yaml.AppendLine("  capacity:");
      yaml.AppendLine($"    storage: {model.Capacity}");
    }

    if (model.AccessModes?.Count > 0)
    {
      yaml.AppendLine("  accessModes:");
      foreach (var mode in model.AccessModes)
      {
        yaml.AppendLine($"    - {mode}");
      }
    }

    if (!string.IsNullOrEmpty(model.ReclaimPolicy))
    {
      yaml.AppendLine($"  persistentVolumeReclaimPolicy: {model.ReclaimPolicy}");
    }

    if (!string.IsNullOrEmpty(model.StorageClassName))
    {
      yaml.AppendLine($"  storageClassName: {model.StorageClassName}");
    }

    if (!string.IsNullOrEmpty(model.VolumeMode))
    {
      yaml.AppendLine($"  volumeMode: {model.VolumeMode}");
    }

    if (model.MountOptions?.Count > 0)
    {
      yaml.AppendLine("  mountOptions:");
      foreach (var option in model.MountOptions)
      {
        yaml.AppendLine($"    - {option}");
      }
    }

    // Add volume source configuration
    if (model.HostPath != null)
    {
      yaml.AppendLine("  hostPath:");
      yaml.AppendLine($"    path: {model.HostPath.Path}");
      if (!string.IsNullOrEmpty(model.HostPath.Type))
      {
        yaml.AppendLine($"    type: {model.HostPath.Type}");
      }
    }
    else if (model.Nfs != null)
    {
      yaml.AppendLine("  nfs:");
      yaml.AppendLine($"    server: {model.Nfs.Server}");
      yaml.AppendLine($"    path: {model.Nfs.Path}");
      if (model.Nfs.ReadOnly)
      {
        yaml.AppendLine($"    readOnly: {model.Nfs.ReadOnly.ToString().ToLowerInvariant()}");
      }
    }
    else if (model.Local != null)
    {
      yaml.AppendLine("  local:");
      yaml.AppendLine($"    path: {model.Local.Path}");
      if (!string.IsNullOrEmpty(model.Local.FsType))
      {
        yaml.AppendLine($"    fsType: {model.Local.FsType}");
      }
    }

    // Add node affinity if specified
    if (model.NodeAffinity?.Required?.Count > 0)
    {
      yaml.AppendLine("  nodeAffinity:");
      yaml.AppendLine("    required:");
      yaml.AppendLine("      nodeSelectorTerms:");
      
      foreach (var term in model.NodeAffinity.Required)
      {
        yaml.AppendLine("        - matchExpressions:");
        if (term.MatchExpressions?.Count > 0)
        {
          foreach (var expr in term.MatchExpressions)
          {
            yaml.AppendLine($"            - key: {expr.Key}");
            yaml.AppendLine($"              operator: {expr.Operator}");
            if (expr.Values?.Count > 0)
            {
              yaml.AppendLine("              values:");
              foreach (var value in expr.Values)
              {
                yaml.AppendLine($"                - {value}");
              }
            }
          }
        }
      }
    }

    return yaml.ToString();
  }
}
