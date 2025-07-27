using System.Runtime.Serialization;

namespace DevantlerTech.KubernetesGenerator.Native.Models;

/// <summary>
/// Represents the verbs that can be used in ClusterRole rules.
/// </summary>
public enum ClusterRoleVerb
{
  /// <summary>
  /// Read the specified resource.
  /// </summary>
  [EnumMember(Value = "get")]
  Get,

  /// <summary>
  /// List resources of the specified type.
  /// </summary>
  [EnumMember(Value = "list")]
  List,

  /// <summary>
  /// Watch resources of the specified type.
  /// </summary>
  [EnumMember(Value = "watch")]
  Watch,

  /// <summary>
  /// Create new resources of the specified type.
  /// </summary>
  [EnumMember(Value = "create")]
  Create,

  /// <summary>
  /// Update existing resources of the specified type.
  /// </summary>
  [EnumMember(Value = "update")]
  Update,

  /// <summary>
  /// Patch existing resources of the specified type.
  /// </summary>
  [EnumMember(Value = "patch")]
  Patch,

  /// <summary>
  /// Delete resources of the specified type.
  /// </summary>
  [EnumMember(Value = "delete")]
  Delete,

  /// <summary>
  /// Delete collections of the specified resource type.
  /// </summary>
  [EnumMember(Value = "deletecollection")]
  DeleteCollection,

  /// <summary>
  /// Bind resources to subjects (for cluster roles).
  /// </summary>
  [EnumMember(Value = "bind")]
  Bind,

  /// <summary>
  /// Escalate permissions.
  /// </summary>
  [EnumMember(Value = "escalate")]
  Escalate,

  /// <summary>
  /// All actions on the specified resource.
  /// </summary>
  [EnumMember(Value = "*")]
  All
}