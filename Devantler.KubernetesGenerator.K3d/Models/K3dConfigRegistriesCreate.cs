namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Configuration for creating a registry in k3d
/// </summary>
public class K3dConfigRegistriesCreate
{
  /// <summary>
  /// Name of the registry
  /// </summary>
  public required string Name { get; set; }

  /// <summary>
  /// Host of the registry
  /// </summary>
  public required string Host { get; set; }

  /// <summary>
  /// Port of the registry
  /// </summary>
  public required string HostPort { get; set; }

  /// <summary>
  /// Proxy configuration for the registry
  /// </summary>
  public K3dConfigRegistriesCreateProxy? Proxy { get; set; }

  /// <summary>
  /// Volumes for the registry
  /// </summary>
  public IReadOnlyCollection<string> Volumes { get; set; } = new List<string>().AsReadOnly();
}
