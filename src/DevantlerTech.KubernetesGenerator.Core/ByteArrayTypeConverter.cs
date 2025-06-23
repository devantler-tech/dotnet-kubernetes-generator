using YamlDotNet.Core;
using YamlDotNet.Serialization;


namespace DevantlerTech.KubernetesGenerator.Core;

sealed class ByteArrayTypeConverter : IYamlTypeConverter
{
  public bool Accepts(Type type) => type == typeof(byte[]);

  public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
  {
    var scalar = parser.Consume<YamlDotNet.Core.Events.Scalar>();
    return Convert.FromBase64String(scalar.Value);
  }

  public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
  {
    byte[]? byteArray = (byte[]?)value;
    string base64String = Convert.ToBase64String(byteArray ?? []);
    emitter.Emit(new YamlDotNet.Core.Events.Scalar(base64String));
  }
}
