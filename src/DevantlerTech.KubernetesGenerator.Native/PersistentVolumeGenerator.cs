using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DevantlerTech.KubernetesGenerator.Native;

/// <summary>
/// A generator for Kubernetes PersistentVolume objects using 'kubectl create -f' commands.
/// </summary>
public class PersistentVolumeGenerator : BaseNativeGenerator<PersistentVolume>
{
  static readonly ISerializer _serializer = new SerializerBuilder()
    .DisableAliases()
    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitEmptyCollections)
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();

  /// <summary>
  /// Generates a persistent volume using kubectl create -f command with YAML input.
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

    // For PersistentVolumes, we need to generate YAML since there's no direct kubectl create command
    // We'll serialize the model to YAML and write it directly to the output file
    string yaml = _serializer.Serialize(model);
    await YamlFileWriter.WriteToFileAsync(outputPath, yaml, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
