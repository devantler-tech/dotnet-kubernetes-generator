using k8s.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Devantler.KubernetesGenerator.Core.Converters;

/// <summary>
/// A JSON converter for <see cref="ResourceQuantity"/> that serializes it as a string.
/// </summary>
public class ResourceQuantityTypeConverter : IYamlTypeConverter
{
  /// <inheritdoc/>
  public bool Accepts(Type type) => type == typeof(ResourceQuantity);

  /// <inheritdoc/>
  public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer) => null;

  /// <inheritdoc/>
  public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
  {
    if (value is ResourceQuantity resourceQuantity)
    {
      emitter?.Emit(new Scalar(resourceQuantity.Value));
    }
  }
}
