using Devantler.KubernetesResourceGenerator.Configs.K3d.Models;
using Devantler.KubernetesResourceGenerator.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Devantler.KubernetesResourceGenerator.Configs.K3d;

/// <summary>
/// A generator for generating K3d config files.
/// </summary>
public class K3dConfigKubernetesResourceGenerator : IKubernetesResourceGenerator<K3dConfig>
{
  readonly ISerializer _serializer = new SerializerBuilder().ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

  /// <inheritdoc />
  public async Task GenerateAsync(K3dConfig model, string outputPath, CancellationToken cancellationToken = default)
  {
    string outputDirectory = Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException("Output path is invalid.");
    if (!Directory.Exists(outputDirectory))
    {
      _ = Directory.CreateDirectory(outputDirectory);
    }
    string yaml = _serializer.Serialize(model);
    await File.WriteAllTextAsync(outputPath, yaml, cancellationToken).ConfigureAwait(false);
  }
}
