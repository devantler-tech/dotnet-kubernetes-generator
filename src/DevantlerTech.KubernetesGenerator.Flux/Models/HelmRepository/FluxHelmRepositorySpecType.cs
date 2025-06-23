using System.Runtime.Serialization;

namespace DevantlerTech.KubernetesGenerator.Flux.Models.HelmRepository;

/// <summary>
/// Type of a Flux HelmRepository.
/// </summary>
public enum FluxHelmRepositorySpecType
{
  /// <summary>
  /// Default Helm repository type.
  /// </summary>
  [EnumMember(Value = "default")]
  Default,
  /// <summary>
  /// Helm repository type for OCI repositories.
  /// </summary>
  [EnumMember(Value = "oci")]
  OCI
}
