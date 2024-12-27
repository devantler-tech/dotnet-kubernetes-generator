using System.ComponentModel;
using Devantler.KubernetesGenerator.Core.Converters;
using Devantler.KubernetesGenerator.Core.Inspectors;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.ObjectFactories;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.System.Text.Json;


namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// A base class for Kubernetes generators.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseKubernetesGenerator<T> : IKubernetesGenerator<T> where T : class
{
  readonly ISerializer _serializer;

  /// <summary>
  /// Initializes a new instance of the <see cref="BaseKubernetesGenerator{T}"/> class.
  /// </summary>
  /// <param name="omitDefaults"></param>
  public BaseKubernetesGenerator(bool omitDefaults = false)
  {
    var serializerBuilder = new SerializerBuilder()
      .DisableAliases()
      .WithTypeInspector(inner => new KubernetesTypeInspector(new SystemTextJsonTypeInspector(inner)))
      .WithTypeConverter(new IntstrIntOrStringTypeConverter())
      .WithTypeConverter(new ResourceQuantityTypeConverter())
      .WithNamingConvention(CamelCaseNamingConvention.Instance);

    serializerBuilder = omitDefaults
      ? serializerBuilder.WithEmissionPhaseObjectGraphVisitor(args => new OmitNullAndDefaultsObjectGraphVisitor(args.InnerVisitor))
      : serializerBuilder.ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull);
    _serializer = serializerBuilder.Build();
  }


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

    if (!yaml.StartsWith("---", StringComparison.Ordinal))
    {
      yaml = $"---{Environment.NewLine}" + yaml;
    }
    if (overwrite)
    {
      await File.WriteAllTextAsync(outputPath, yaml, cancellationToken).ConfigureAwait(false);
    }
    else
    {
      await File.AppendAllTextAsync(outputPath, yaml, cancellationToken).ConfigureAwait(false);
    }
  }
}

class OmitNullAndDefaultsObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) : ChainedObjectGraphVisitor(nextVisitor)
{
  static object? GetDefault(Type type) => new DefaultObjectFactory().CreatePrimitive(type);

  public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context, ObjectSerializer serializer)
  {
    object? defaultValue = key.GetCustomAttribute<DefaultValueAttribute>()?.Value ?? GetDefault(key.Type);
    return value.Value is not null && !Equals(value.Value, defaultValue) && base.EnterMapping(key, value, context, serializer);
  }
}
