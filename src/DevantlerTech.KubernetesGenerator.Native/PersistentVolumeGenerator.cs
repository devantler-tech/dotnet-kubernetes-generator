using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Converters;
using DevantlerTech.KubernetesGenerator.Core.Inspectors;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.System.Text.Json;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PersistentVolume objects using 'kubectl create -f' commands.
/// </summary>
public class PersistentVolumeGenerator : BaseNativeGenerator<PersistentVolume>
{
  readonly ISerializer _serializer = new SerializerBuilder()
    .DisableAliases()
    .WithTypeInspector(inner => new KubernetesTypeInspector(new CommentGatheringTypeInspector(new SystemTextJsonTypeInspector(inner))))
    .WithTypeConverter(new IntstrIntOrStringTypeConverter())
    .WithTypeConverter(new ResourceQuantityTypeConverter())
    .WithEmissionPhaseObjectGraphVisitor(inner => new KubernetesObjectGraphVisitor<V1PersistentVolume>(new CommentsObjectGraphVisitor(inner.InnerVisitor), Activator.CreateInstance<V1PersistentVolume>()))
    .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

  /// <summary>
  /// Generates a persistent volume using kubectl create -f command.
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

    // Validate required fields
    if (string.IsNullOrEmpty(model.Metadata?.Name))
    {
      throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the persistent volume name.");
    }

    // Convert the custom model to V1PersistentVolume for serialization
    var v1PersistentVolume = ConvertToV1PersistentVolume(model);

    // Serialize the model to YAML
    string yaml = _serializer.Serialize(v1PersistentVolume);

    // Use kubectl with YAML input
    await RunKubectlWithYamlAsync(outputPath, overwrite, yaml, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>
  /// Converts a custom PersistentVolume model to V1PersistentVolume for serialization.
  /// </summary>
  /// <param name="model">The custom PersistentVolume model.</param>
  /// <returns>The V1PersistentVolume object.</returns>
  static V1PersistentVolume ConvertToV1PersistentVolume(PersistentVolume model)
  {
    var v1PersistentVolume = new V1PersistentVolume
    {
      ApiVersion = "v1",
      Kind = "PersistentVolume",
      Metadata = model.Metadata,
      Spec = new V1PersistentVolumeSpec
      {
        AccessModes = model.AccessModes,
        Capacity = model.Capacity,
        StorageClassName = model.StorageClassName,
        PersistentVolumeReclaimPolicy = model.PersistentVolumeReclaimPolicy,
        MountOptions = model.MountOptions,
        NodeAffinity = model.NodeAffinity,
        ClaimRef = model.ClaimRef
      }
    };

    // Set volume source based on what's provided
    if (model.HostPath != null)
    {
      v1PersistentVolume.Spec.HostPath = model.HostPath;
    }
    else if (model.Nfs != null)
    {
      v1PersistentVolume.Spec.Nfs = model.Nfs;
    }

    return v1PersistentVolume;
  }

  /// <summary>
  /// Runs kubectl with YAML input passed via stdin.
  /// </summary>
  /// <param name="outputPath">The output path for the generated YAML.</param>
  /// <param name="overwrite">Whether to overwrite existing files.</param>
  /// <param name="yamlInput">The YAML content to pass via stdin.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  static async Task RunKubectlWithYamlAsync(string outputPath, bool overwrite, string yamlInput, CancellationToken cancellationToken) =>
    // For kubectl create -f -, we need to pass YAML to stdin
    // For now, we'll write the YAML directly to the output file since that's what we want to generate
    await YamlFileWriter.WriteToFileAsync(outputPath, yamlInput, overwrite, cancellationToken).ConfigureAwait(false);
}
