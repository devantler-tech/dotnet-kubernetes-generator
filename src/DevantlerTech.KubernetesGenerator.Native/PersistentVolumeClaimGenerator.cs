using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PersistentVolumeClaim objects using 'kubectl create -f' commands.
/// </summary>
public class PersistentVolumeClaimGenerator : BaseNativeGenerator<V1PersistentVolumeClaim>
{
  static readonly string[] _defaultArgs = ["create", "-f"];
  
  /// <summary>
  /// List of temporary files created during generation that need to be cleaned up.
  /// </summary>
  readonly List<string> _temporaryFiles = [];

  /// <summary>
  /// YAML serializer for creating the PVC manifest.
  /// </summary>
  readonly ISerializer _serializer = new SerializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();

  /// <summary>
  /// Generates a PersistentVolumeClaim using kubectl create -f command.
  /// </summary>
  /// <param name="model">The PersistentVolumeClaim object.</param>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
  /// <exception cref="KubernetesGeneratorException">Thrown when required parameters are missing.</exception>
  public override async Task GenerateAsync(V1PersistentVolumeClaim model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(model);

    var args = new ReadOnlyCollection<string>(
      [.. _defaultArgs, .. await AddOptionsAsync(model, cancellationToken).ConfigureAwait(false)]
    );
    string errorMessage = $"Failed to create PersistentVolumeClaim '{model.Metadata?.Name}' using kubectl";
    await RunKubectlAsync(outputPath, overwrite, args, errorMessage, cancellationToken).ConfigureAwait(false);

    CleanUpTemporaryFiles();
  }

  /// <summary>
  /// Builds the kubectl arguments for creating a PersistentVolumeClaim from a V1PersistentVolumeClaim object.
  /// </summary>
  /// <param name="model">The V1PersistentVolumeClaim object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The kubectl arguments.</returns>
  async Task<ReadOnlyCollection<string>> AddOptionsAsync(V1PersistentVolumeClaim model, CancellationToken cancellationToken = default)
  {
    var args = new List<string>();

    // Require that a PVC name is provided
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to specify the PersistentVolumeClaim name.");
    }

    // Create temporary YAML file for the PVC
    string tempYamlPath = await CreateTemporaryYamlFileAsync(model, cancellationToken).ConfigureAwait(false);
    args.Add(tempYamlPath);

    return args.AsReadOnly();
  }

  /// <summary>
  /// Creates a temporary YAML file from the V1PersistentVolumeClaim object.
  /// </summary>
  /// <param name="model">The V1PersistentVolumeClaim object.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The path to the temporary YAML file.</returns>
  async Task<string> CreateTemporaryYamlFileAsync(V1PersistentVolumeClaim model, CancellationToken cancellationToken = default)
  {
    // Ensure the model has the required API version and kind
    model.ApiVersion ??= "v1";
    model.Kind ??= "PersistentVolumeClaim";

    string yaml = _serializer.Serialize(model);
    string tempPath = Path.Combine(Path.GetTempPath(), $"pvc-{Guid.NewGuid()}.yaml");
    await File.WriteAllTextAsync(tempPath, yaml, cancellationToken).ConfigureAwait(false);
    _temporaryFiles.Add(tempPath);
    return tempPath;
  }

  /// <summary>
  /// Cleans up temporary files created during the generation process.
  /// </summary>
  void CleanUpTemporaryFiles()
  {
    foreach (string tempFile in _temporaryFiles)
    {
      if (File.Exists(tempFile))
      {
        File.Delete(tempFile);
      }
    }
    _temporaryFiles.Clear();
  }
}
