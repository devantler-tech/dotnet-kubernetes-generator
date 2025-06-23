using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;

namespace DevantlerTech.KubernetesGenerator.Core.Inspectors;

/// <summary>
/// A type inspector that prioritizes properties of Kubernetes resources, so they come first, and so they are ordered according to conventions.
/// Thanks to @EdwardCooke for the initial implementation.
/// </summary>
/// <param name="inner"></param>
public sealed class KubernetesTypeInspector(ITypeInspector inner) : ITypeInspector
{
  /// <summary>
  /// Gets the inner type inspector.
  /// </summary>
  public ITypeInspector Inner { get; } = inner;

  /// <summary>
  /// Gets the name of the enum value.
  /// </summary>
  /// <param name="enumType"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  public string GetEnumName(Type enumType, string name) => Inner.GetEnumName(enumType, name);

  /// <summary>
  /// Gets the value of the enum.
  /// </summary>
  /// <param name="enumValue"></param>
  /// <returns></returns>
  public string GetEnumValue(object enumValue) => Inner.GetEnumValue(enumValue);

  /// <summary>
  /// Gets the properties of the system type.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
  {
    ArgumentNullException.ThrowIfNull(type, nameof(type));
    var properties = Inner.GetProperties(type, container).ToList();
    var skipped = new List<string>();
    if (type.FullName?.StartsWith("k8s.Models", StringComparison.OrdinalIgnoreCase) ?? true)
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
      if (property.Type == typeof(Uri))
      {
        skipped.Add(property.Name);
        yield return new PropertyDescriptor(property) { TypeOverride = typeof(string) };
      }
      else if (!skipped.Contains(property.Name))
      {
        yield return property;
      }
    }
  }

  /// <summary>
  /// Gets the property of the type.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="container"></param>
  /// <param name="name"></param>
  /// <param name="ignoreUnmatched"></param>
  /// <param name="caseInsensitivePropertyMatching"></param>
  /// <returns></returns>
  public IPropertyDescriptor GetProperty(Type type, object? container, string name, [MaybeNullWhen(true)] bool ignoreUnmatched, bool caseInsensitivePropertyMatching) => Inner.GetProperty(type, container, name, ignoreUnmatched, caseInsensitivePropertyMatching);
}
