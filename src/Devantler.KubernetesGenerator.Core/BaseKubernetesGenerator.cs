using Devantler.KubernetesGenerator.Core.Converters;
using Devantler.KubernetesGenerator.Core.Inspectors;
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
    .DisableAliases()
    .WithTypeInspector(inner => new KubernetesTypeInspector(new SystemTextJsonTypeInspector(inner)))
    .WithTypeConverter(new IntstrIntOrStringTypeConverter())
    .WithTypeConverter(new ResourceQuantityTypeConverter())
    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
    .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

  /// <summary>
  /// Generates Kubernetes resources from the given model and writes them to the output path.
  /// </summary>
  /// <param name="model"></param>
  /// <param name="outputPath"></param>
  /// <param name="overwrite"></param>
  /// <param name="cancellationToken"></param>
  public async Task GenerateAsync(T model, string outputPath, bool overwrite = false, CancellationToken cancellationToken = default)
  {
    string outputDirectory = Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException("Output path is invalid.");
    if (!Directory.Exists(outputDirectory))
    {
      _ = Directory.CreateDirectory(outputDirectory);
    }
    string yaml = _serializer.Serialize(model);

    await YamlFileWriter.WriteToFileAsync(outputPath, yaml, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
