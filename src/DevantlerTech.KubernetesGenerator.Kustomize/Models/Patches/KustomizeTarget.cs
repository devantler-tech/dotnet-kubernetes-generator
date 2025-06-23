#pragma warning disable CA2227
namespace DevantlerTech.KubernetesGenerator.Kustomize.Models.Patches;
/// <summary>
/// The target resource(s) to apply the patch to.
/// </summary>
public class KustomizeTarget
{
  /// <summary>
  /// The kind of the resource.
  /// </summary>
  public required string Kind { get; set; }

  /// <summary>
  /// The name of the resource.
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// The namespace of the resource.
  /// </summary>
  public string? Namespace { get; set; }

  /// <summary>
  /// The group that the resource belongs to.
  /// </summary>
  public string? Group { get; set; }

  /// <summary>
  /// The version of the resource.
  /// </summary>
  public string? Version { get; set; }

  /// <summary>
  /// A label selector to match the resource by labels. (e.g. `app=nginx`)
  /// </summary>
  public string? LabelSelector { get; set; }

  /// <summary>
  /// An annotation selector to match the resource by annotations. (e.g. `app=nginx`)
  /// </summary>
  public string? AnnotationSelector { get; set; }
}
