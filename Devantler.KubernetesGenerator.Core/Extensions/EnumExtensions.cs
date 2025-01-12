// TODO Move to Devantler.Commons.Extensions.EnumExtensions.cs
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Devantler.KubernetesGenerator.Core.Extensions;

/// <summary>
/// Extensions for Enum.
/// </summary>
public static class EnumExtensions
{
  /// <summary>
  /// Get the description of an enum.
  /// </summary>
  public static string GetDescriptionOrDefault(this Enum value)
  {
    var field = value.GetType().GetField(value.ToString());
    var descriptionAttribute = field?.GetCustomAttribute<DescriptionAttribute>();
    return descriptionAttribute?.Description ?? value.ToString();
  }

  /// <summary>
  /// Get the enum member value from an enum.
  /// </summary>
  public static string GetEnumMemberValueOrDefault(this Enum value)
  {
    var field = value.GetType().GetField(value.ToString());
    var enumMemberAttribute = field?.GetCustomAttribute<EnumMemberAttribute>();
    return enumMemberAttribute?.Value ?? value.ToString();
  }
}
