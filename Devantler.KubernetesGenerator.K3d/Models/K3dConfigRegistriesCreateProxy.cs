namespace Devantler.KubernetesGenerator.K3d.Models;

/// <summary>
/// Proxy configuration for the registry
/// </summary>
public class K3dConfigRegistriesCreateProxy
{
  /// <summary>
  /// Remote URL of the proxy
  /// </summary>
#pragma warning disable CA1056 // URI-like properties should not be strings
  public required string RemoteURL { get; set; }
#pragma warning restore CA1056 // URI-like properties should not be strings

  /// <summary>
  /// Username for the proxy
  /// </summary>
  public string? Username { get; set; }

  /// <summary>
  /// Password for the proxy
  /// </summary>
  public string? Password { get; set; }
}
