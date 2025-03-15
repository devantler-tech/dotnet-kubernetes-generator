using System.Text.RegularExpressions;
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
public partial class BaseKubernetesGenerator<T> : IKubernetesGenerator<T> where T : class
{
  readonly ISerializer _serializer = new SerializerBuilder()
    .DisableAliases()
    .WithTypeInspector(inner => new KubernetesTypeInspector(new SystemTextJsonTypeInspector(inner)))
    .WithTypeConverter(new IntstrIntOrStringTypeConverter())
    .WithTypeConverter(new ResourceQuantityTypeConverter())
    .WithTypeConverter(new ByteArrayTypeConverter())
    .WithEmissionPhaseObjectGraphVisitor(inner => new KubernetesObjectGraphVisitor<T>(inner.InnerVisitor, Activator.CreateInstance<T>()))
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

    var directory = Path.GetDirectoryName(outputPath);
    if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
    {
      _ = Directory.CreateDirectory(directory);
    }

    string yaml = _serializer.Serialize(model);
    yaml = EmptyObjectRegex().Replace(yaml, string.Empty);
    yaml = yaml.TrimEnd(Environment.NewLine.ToCharArray());
    yaml = EmptyObjectKeyRegex().Replace(yaml, "$1 {}");
    yaml = yaml.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine, StringComparison.Ordinal);
    await YamlFileWriter.WriteToFileAsync(outputPath, yaml, overwrite, cancellationToken).ConfigureAwait(false);
  }

  [GeneratedRegex(@"^\s*\w+\s*:\s*{\s*}\s*$", RegexOptions.Multiline)]
  private static partial Regex EmptyObjectRegex();

  [GeneratedRegex(@"^(\s*\w+\s*:\s*)(?![\r\n]+[-]*\s+)$", RegexOptions.Multiline)]
  private static partial Regex EmptyObjectKeyRegex();
}
