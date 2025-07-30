using System.Net;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Core.Converters;

/// <summary>
/// A YAML converter for <see cref="IPNetwork"/> that serializes it as a CIDR string.
/// </summary>
public class IPNetworkTypeConverter : IYamlTypeConverter
{
  /// <inheritdoc/>
  public bool Accepts(Type type) => type == typeof(IPNetwork) || type == typeof(IPNetwork?);

  /// <inheritdoc/>
  public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer) =>
    parser?.Current is Scalar scalar && IPNetwork.TryParse(scalar.Value, out var network) ? network : null;

  /// <inheritdoc/>
  public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
  {
    if (value is IPNetwork network)
    {
      emitter?.Emit(new Scalar(network.ToString()));
    }
  }
}