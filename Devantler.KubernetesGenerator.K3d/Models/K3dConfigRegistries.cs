namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Configuration for registries in k3d
/// </summary>
public class K3dConfigRegistries
{
  /// <summary>
  /// Configuration for creating a registry in k3d
  /// </summary>
  public K3dConfigRegistriesCreate? Create { get; set; }

  /// <summary>
  /// Configuration for using a registry in k3d
  /// </summary>
  public IEnumerable<string>? Use { get; set; }

  /// <summary>
  /// A mirror registry configuration (A path to a registries.yaml file or a string containing the content of a registries.yaml file)
  /// </summary>
  public string? Config { get; set; }
}
