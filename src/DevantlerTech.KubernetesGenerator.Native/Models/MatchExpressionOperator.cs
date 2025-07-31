namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Enum for supported match expression operators.
/// </summary>
public enum NativeMatchExpressionOperator
{
  /// <summary>
  /// The key should be present and equal to one of the specified values.
  /// </summary>
  In,

  /// <summary>
  /// The key should be present and not equal to any of the specified values.
  /// </summary>
  NotIn,

  /// <summary>
  /// The key should be present.
  /// </summary>
  Exists,

  /// <summary>
  /// The key should not be present.
  /// </summary>
  DoesNotExist,

  /// <summary>
  /// The key should be present and its value should be greater than the specified value.
  /// </summary>
  Gt,

  /// <summary>
  /// The key should be present and its value should be less than the specified value.
  /// </summary>
  Lt
}
