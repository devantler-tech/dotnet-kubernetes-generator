using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Devantler.KubernetesGenerator.Core;


/// <summary>
/// A descriptor for objects that includes comments for YAML serialization.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommentsObjectDescriptor"/> class.
/// </remarks>
/// <param name="innerDescriptor"></param>
/// <param name="comment"></param>
public sealed class CommentsObjectDescriptor(IObjectDescriptor innerDescriptor, string comment) : IObjectDescriptor
{
  private readonly IObjectDescriptor _innerDescriptor = innerDescriptor;

  /// <summary>
  /// Gets the comment associated with the object descriptor.
  /// </summary>
  public string Comment { get; private set; } = comment;

  /// <summary>
  /// Gets the value of the object descriptor.
  /// </summary>
  public object? Value => _innerDescriptor.Value;

  /// <summary>
  /// Gets the type of the object descriptor.
  /// </summary>
  public Type Type { get { return _innerDescriptor.Type; } }

  /// <summary>
  /// Gets the static type of the object descriptor.
  /// </summary>
  public Type StaticType { get { return _innerDescriptor.StaticType; } }

  /// <summary>
  /// Gets the scalar style of the object descriptor.
  /// </summary>
  public ScalarStyle ScalarStyle { get { return _innerDescriptor.ScalarStyle; } }
}
