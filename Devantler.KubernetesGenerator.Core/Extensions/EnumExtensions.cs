using System.ComponentModel;
using System.Reflection;

namespace Devantler.KubernetesGenerator.Core.Extensions;

/// <summary>
/// Extensions for Enum.
/// </summary>
public static class EnumExtensions
{
  /// <summary>
  /// Get the description of an enum value.
  /// </summary>
  public static string GetDescription(this Enum value)
  {
    var field = value.GetType().GetField(value.ToString());
    var descriptionAttribute = field?.GetCustomAttribute<DescriptionAttribute>();
    return descriptionAttribute?.Description ?? value.ToString();
  }
}
