using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;

namespace DevantlerTech.KubernetesGenerator.Core;

/// <summary>
/// A visitor that handles default values for Kubernetes objects.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="KubernetesObjectGraphVisitor{T}"/> class.
/// </remarks>
/// <param name="nextVisitor"></param>
/// <param name="model"></param>
public sealed class KubernetesObjectGraphVisitor<T>(IObjectGraphVisitor<IEmitter> nextVisitor, T model) : ChainedObjectGraphVisitor(nextVisitor)
{
  /// <summary>
  /// Enters a mapping.
  /// </summary>
  /// <param name="key"></param>
  /// <param name="value"></param>
  /// <param name="context"></param>
  /// <param name="serializer"></param>
  /// <returns></returns>
  public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context, ObjectSerializer serializer)
  {
    ArgumentNullException.ThrowIfNull(value);
    ArgumentNullException.ThrowIfNull(key);

    // Skip null values
    if (value.Value is null)
      return false;

    // Skip protected Kubernetes properties
    if (key.Name.Equals("apiVersion", StringComparison.OrdinalIgnoreCase)
      || key.Name.Equals("kind", StringComparison.OrdinalIgnoreCase)
      || key.Name.Equals("metadata", StringComparison.OrdinalIgnoreCase))
    {
      return true;
    }

    // Skip default initialization values
    return (model is null || !SkipDefaultInitializationValues(key, value, model)) && base.EnterMapping(key, value, context, serializer);
  }

  static bool SkipDefaultInitializationValues(IPropertyDescriptor key, IObjectDescriptor value, object obj)
  {
    foreach (var property in obj.GetType().GetProperties())
    {
      if (!property.Name.Equals(key.Name, StringComparison.OrdinalIgnoreCase))
      {
        if (property.PropertyType.GetConstructor(Type.EmptyTypes) is null)
          continue;
        object? nextObject = Activator.CreateInstance(property.PropertyType);
        if (nextObject is null)
          continue;
        if (SkipDefaultInitializationValues(key, value, nextObject))
          return true;
        continue;
      }

      if (property.GetCustomAttribute<RequiredMemberAttribute>() is not null || property.GetCustomAttribute<RequiredAttribute>() is not null)
        return false;

      object? currentValue = value.Value;
      object? defaultValue = property.GetValue(obj);
      return currentValue != null && currentValue.Equals(defaultValue);
    }
    return false;
  }
}
