using System.Diagnostics.CodeAnalysis;
using DevantlerTech.KubernetesGenerator.Core.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Models.StatefulSet;

/// <summary>
/// Represents a Kubernetes StatefulSet.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "This is a specific Kubernetes resource model")]
public class NativeStatefulSet
{
  /// <summary>
  /// Gets or sets the API version of this StatefulSet.
  /// </summary>
  public string ApiVersion { get; set; } = "apps/v1";

  /// <summary>
  /// Gets or sets the kind of this resource.
  /// </summary>
  public string Kind { get; set; } = "StatefulSet";

  /// <summary>
  /// Gets or sets the metadata for the StatefulSet.
  /// </summary>
  public required NamespacedMetadata Metadata { get; set; }

  /// <summary>
  /// Gets or sets the specification for the StatefulSet.
  /// </summary>
  public required NativeStatefulSetSpec Spec { get; init; }
}
