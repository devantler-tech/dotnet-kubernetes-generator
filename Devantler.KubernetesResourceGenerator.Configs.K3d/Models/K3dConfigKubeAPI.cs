namespace Devantler.KubernetesResourceGenerator.Configs.K3d.Models;

/// <summary>
/// Configuration for the KubeAPI.
/// </summary>
public class K3dConfigKubeAPI
{
  /// <summary>
  /// The host of the KubeAPI.
  /// </summary>
  public string? Host { get; set; }

  /// <summary>
  /// The host IP of the KubeAPI.
  /// </summary>
  public string? HostIP { get; set; }

  /// <summary>
  /// The host port of the KubeAPI.
  /// </summary>
  public string? HostPort { get; set; }
}
