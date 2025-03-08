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
    ArgumentNullException.ThrowIfNull(model);

    var directory = Path.GetDirectoryName(outputPath);
    if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
    {
      _ = Directory.CreateDirectory(directory);
    }
    var defaultObject = Activator.CreateInstance<T>();
    foreach (var property in model.GetType().GetProperties())
    {
      if (property.Name == "ApiVersion" || property.Name == "Kind")
      {
        continue;
      }

      var defaultValue = property.GetValue(defaultObject);
      var value = property.GetValue(model);
      if (value != null && value.Equals(defaultValue) && property.CanWrite)
      {
        property.SetValue(model, null);
      }
    }
    string yaml = _serializer.Serialize(model);

    await YamlFileWriter.WriteToFileAsync(outputPath, yaml, overwrite, cancellationToken).ConfigureAwait(false);
  }
}
