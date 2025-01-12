// TODO Move to Devantler.Commons.Extensions.ListExtensions.cs
namespace Devantler.KubernetesGenerator.Core.Extensions;

/// <summary>
/// Extensions for List.
/// </summary>
public static class ListExtensions
{
  /// <summary>
  /// Adds an argument to the list if the value is not null.
  /// </summary>
  /// <param name="arguments"></param>
  /// <param name="format"></param>
  /// <param name="values"></param>
  public static void AddIfNotNull(this List<string> arguments, string format, params object?[] values)
  {
    if (values.All(value => value != null))
      arguments.Add(string.Format(format, values));
  }
}
