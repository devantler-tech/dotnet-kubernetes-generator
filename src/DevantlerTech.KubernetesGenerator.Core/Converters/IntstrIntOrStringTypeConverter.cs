using k8s.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Core.Converters;

/// <summary>
/// A YAML converter for <see cref="IntstrIntOrString"/> that serializes it as a string.
/// </summary>
public class IntstrIntOrStringTypeConverter : IYamlTypeConverter
{
  /// <inheritdoc/>
  public bool Accepts(Type type) => type == typeof(IntstrIntOrString);

  /// <inheritdoc/>
  public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer) =>
    parser?.Current is Scalar scalar ? new IntstrIntOrString(scalar.Value) : (object?)null;

  /// <inheritdoc/>
  public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
  {
    if (value is IntstrIntOrString resourceQuantity)
    {
      emitter?.Emit(new Scalar(resourceQuantity.Value));
    }
  }
}
