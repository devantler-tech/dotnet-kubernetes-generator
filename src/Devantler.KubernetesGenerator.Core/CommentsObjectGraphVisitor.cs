using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;

namespace Devantler.KubernetesGenerator.Core;


/// <summary>
/// An object graph visitor that adds comments to the YAML output.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CommentsObjectGraphVisitor"/> class.
/// </remarks>
/// <param name="nextVisitor"></param>
public class CommentsObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor) : ChainedObjectGraphVisitor(nextVisitor)
{
  /// <summary>
  /// Enters a mapping in the object graph and emits a comment if the value is a <see cref="CommentsObjectDescriptor"/>.
  /// </summary>
  /// <param name="key"></param>
  /// <param name="value"></param>
  /// <param name="context"></param>
  /// <param name="serializer"></param>
  /// <returns></returns>
  public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context, ObjectSerializer serializer)
  {
    ArgumentNullException.ThrowIfNull(context);
    if (value is CommentsObjectDescriptor commentsDescriptor && commentsDescriptor.Comment != null)
      context.Emit(new Comment(commentsDescriptor.Comment, false));
    return base.EnterMapping(key, value, context, serializer);
  }
}
