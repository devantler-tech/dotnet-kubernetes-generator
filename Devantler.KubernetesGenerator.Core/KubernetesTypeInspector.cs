using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;

namespace Devantler.KubernetesGenerator.Core;

/// <summary>
/// A type inspector that prioritizes properties of Kubernetes resources, so they come first, and so they are ordered according to conventions.
/// Thanks to @EdwardCooke for the initial implementation.
/// </summary>
/// <param name="inner"></param>
sealed class KubernetesTypeInspector(ITypeInspector inner) : ITypeInspector
{
  public ITypeInspector Inner { get; } = inner;

  public string GetEnumName(Type enumType, string name) => Inner.GetEnumName(enumType, name);

  public string GetEnumValue(object enumValue) => Inner.GetEnumValue(enumValue);

  public IEnumerable<IPropertyDescriptor> GetProperties(Type systemType, object? container)
  {
    var properties = Inner.GetProperties(systemType, container).ToList();
    var skipped = new List<string>();
    if (systemType.FullName?.StartsWith("k8s.Models", StringComparison.OrdinalIgnoreCase) ?? true)
    {
      foreach (string? propName in new[] { "apiVersion", "kind", "metadata", "type" })
      {
        var property = properties.SingleOrDefault(p => p.Name == propName);
        if (property != null)
        {
          skipped.Add(propName);
          yield return property;
        }
      }
    }

    foreach (var property in properties)
    {
      if (!skipped.Contains(property.Name))
      {
        yield return property;
      }
    }
  }

  public IPropertyDescriptor GetProperty(Type type, object? container, string name, [MaybeNullWhen(true)] bool ignoreUnmatched, bool caseInsensitivePropertyMatching) => Inner.GetProperty(type, container, name, ignoreUnmatched, caseInsensitivePropertyMatching);
}
