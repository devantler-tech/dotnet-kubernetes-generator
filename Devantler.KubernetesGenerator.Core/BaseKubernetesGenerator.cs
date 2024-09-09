using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.System.Text.Json;

namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// A base class for Kubernetes generators.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseKubernetesGenerator<T> : IKubernetesGenerator<T> where T : class
{
  readonly ISerializer _serializer = new SerializerBuilder()
    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .WithTypeInspector(x => new SystemTextJsonTypeInspector(x)).Build();

  /// <summary>
  /// Generates Kubernetes resources from the given model and writes them to the output path.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="cancellationToken"></param>
  public async Task GenerateAsync(T model, string outputPath, CancellationToken cancellationToken = default)
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
