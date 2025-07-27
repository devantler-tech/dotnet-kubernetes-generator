namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the verbs that can be used in ClusterRole rules.
/// </summary>
public enum ClusterRoleVerb
{
  /// <summary>
  /// Read the specified resource.
  /// </summary>
  Get,

  /// <summary>
  /// List resources of the specified type.
  /// </summary>
  List,

  /// <summary>
  /// Watch resources of the specified type.
  /// </summary>
  Watch,

  /// <summary>
  /// Create new resources of the specified type.
  /// </summary>
  Create,

  /// <summary>
  /// Update existing resources of the specified type.
  /// </summary>
  Update,

  /// <summary>
  /// Patch existing resources of the specified type.
  /// </summary>
  Patch,

  /// <summary>
  /// Delete resources of the specified type.
  /// </summary>
  Delete,

  /// <summary>
  /// Delete collections of the specified resource type.
  /// </summary>
  DeleteCollection,

  /// <summary>
  /// Bind resources to subjects (for cluster roles).
  /// </summary>
  Bind,

  /// <summary>
  /// Escalate permissions.
  /// </summary>
  Escalate,

  /// <summary>
  /// All actions on the specified resource.
  /// </summary>
  All
}