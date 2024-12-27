using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectFactories;
using YamlDotNet.Serialization.ObjectGraphVisitors;


namespace Devantler.KubernetesGenerator.Core.Visitors;

class OmitNullAndDefaultsObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) : ChainedObjectGraphVisitor(nextVisitor)
{
  static object? GetDefault(Type type) => new DefaultObjectFactory().CreatePrimitive(type);

  public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context, ObjectSerializer serializer)
  {
    object? defaultValue = key.GetCustomAttribute<DefaultValueAttribute>()?.Value ?? GetDefault(key.Type);
    return value.Value is not null && !Equals(value.Value, defaultValue) && base.EnterMapping(key, value, context, serializer);
  }
}
