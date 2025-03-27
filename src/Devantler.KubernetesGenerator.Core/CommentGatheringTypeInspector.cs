using System.ComponentModel;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace Devantler.KubernetesGenerator.Core;


/// <summary>
/// A type inspector that gathers comments from properties and adds them to the YAML output.
/// </summary>
public class CommentGatheringTypeInspector : TypeInspectorSkeleton
{
  private readonly ITypeInspector _innerTypeDescriptor;

  /// <summary>
  /// Initializes a new instance of the <see cref="CommentGatheringTypeInspector"/> class.
  /// </summary>
  /// <param name="innerTypeDescriptor"></param>
  public CommentGatheringTypeInspector(ITypeInspector innerTypeDescriptor)
  {
    ArgumentNullException.ThrowIfNull(innerTypeDescriptor);

    _innerTypeDescriptor = innerTypeDescriptor;
  }

  /// <summary>
  /// Gets the name of the specified enum.
  /// </summary>
  /// <param name="enumType"></param>
  /// <param name="name"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override string GetEnumName(Type enumType, string name) => _innerTypeDescriptor.GetEnumName(enumType, name);

  /// <summary>
  /// Gets the value of the specified enum enum.
  /// </summary>
  /// <param name="enumValue"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override string GetEnumValue(object enumValue) => _innerTypeDescriptor.GetEnumValue(enumValue);

  /// <summary>
  /// Gets the properties of the specified type and container.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object? container)
  {
    return _innerTypeDescriptor
      .GetProperties(type, container)
      .Select(d => new CommentsPropertyDescriptor(d));
  }

  private sealed class CommentsPropertyDescriptor(IPropertyDescriptor baseDescriptor) : IPropertyDescriptor
  {
    private readonly IPropertyDescriptor _baseDescriptor = baseDescriptor;

    public string Name { get; set; } = baseDescriptor.Name;

    public Type Type { get { return _baseDescriptor.Type; } }

    public Type? TypeOverride
    {
      get => _baseDescriptor.TypeOverride; set => _baseDescriptor.TypeOverride = value;
    }

    public int Order { get; set; }

    public ScalarStyle ScalarStyle
    {
      get { return _baseDescriptor.ScalarStyle; }
      set { _baseDescriptor.ScalarStyle = value; }
    }

    public bool CanWrite { get { return _baseDescriptor.CanWrite; } }

    public bool AllowNulls => _baseDescriptor.AllowNulls;

    ScalarStyle IPropertyDescriptor.ScalarStyle { get => _baseDescriptor.ScalarStyle; set => _baseDescriptor.ScalarStyle = value; }
    public bool Required => _baseDescriptor.Required;

    public Type? ConverterType => _baseDescriptor.ConverterType;

    public void Write(object target, object? value) => _baseDescriptor.Write(target, value);

    public T GetCustomAttribute<T>() where T : Attribute => _baseDescriptor.GetCustomAttribute<T>()!;

    public IObjectDescriptor Read(object target)
    {
      var description = _baseDescriptor.GetCustomAttribute<DescriptionAttribute>();
      return description != null
        ? new CommentsObjectDescriptor(_baseDescriptor.Read(target), description.Description)
        : _baseDescriptor.Read(target);
    }
  }
}
