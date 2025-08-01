using DevantlerTech.KubernetesGenerator.Core.Converters;
using DevantlerTech.KubernetesGenerator.Core.Inspectors;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.System.Text.Json;


namespace DevantlerTech.KubernetesGenerator.Core;

/// <summary>
/// A base class for Kubernetes generators.
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class Generator<T> : IKubernetesGenerator<T> where T : class
{
  readonly ISerializer _serializer = new SerializerBuilder()
    .DisableAliases()
    .WithTypeInspector(inner => new KubernetesTypeInspector(new CommentGatheringTypeInspector(new SystemTextJsonTypeInspector(inner))))
    .WithTypeConverter(new IntstrIntOrStringTypeConverter())
    .WithTypeConverter(new ResourceQuantityTypeConverter())
    .WithTypeConverter(new ByteArrayTypeConverter())
    .WithTypeConverter(new IPNetworkTypeConverter())
    .WithEmissionPhaseObjectGraphVisitor(inner => new KubernetesObjectGraphVisitor<T>(new CommentsObjectGraphVisitor(inner.InnerVisitor), Activator.CreateInstance<T>()))
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
    ArgumentNullException.ThrowIfNull(model);

    string? directory = Path.GetDirectoryName(outputPath);
    if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
    {
      _ = Directory.CreateDirectory(directory);
    }

    string yaml = _serializer.Serialize(model);
    await YamlFileWriter.WriteToFileAsync(outputPath, yaml, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
