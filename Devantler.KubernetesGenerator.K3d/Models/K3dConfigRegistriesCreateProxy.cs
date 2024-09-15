namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Proxy configuration for the registry
/// </summary>
public class K3dConfigRegistriesCreateProxy
{
  /// <summary>
  /// Remote URL of the proxy
  /// </summary>
  public required Uri RemoteURL { get; set; }

  /// <summary>
  /// Username for the proxy
  /// </summary>
  public string? Username { get; set; }

  /// <summary>
  /// Password for the proxy
  /// </summary>
  public string? Password { get; set; }
}
