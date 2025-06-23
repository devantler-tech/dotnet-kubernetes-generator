using DevantlerTech.KubernetesGenerator.K3d.Models.Options;
using DevantlerTech.KubernetesGenerator.K3d.Models.Registries;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.K3d.Models;

/// <summary>
/// A configuration for a K3d cluster.
/// </summary>
public class K3dConfig
{

  /// <summary>
  /// API version for the K3d cluster.
  /// </summary>
  public string ApiVersion { get; set; } = "k3d.io/v1alpha5";

  /// <summary>
  /// Kind for the K3d cluster.
  /// </summary>
  public string Kind { get; set; } = "Simple";

  /// <summary>
  /// Metadata for the K3d cluster.
  /// </summary>
  public required V1ObjectMeta Metadata { get; set; }

  /// <summary>
  /// Number of servers in the K3d cluster.
  /// </summary>
  public int? Servers { get; set; }

  /// <summary>
  /// Number of agents in the K3d cluster.
  /// </summary>
  public int? Agents { get; set; }

  /// <summary>
  /// Configuration for the KubeAPI.
  /// </summary>
  public K3dKubeAPI? KubeAPI { get; set; }

  /// <summary>
  /// Image to use for the K3d cluster.
  /// </summary>
  public string? Image { get; set; }

  /// <summary>
  /// Network to use for the K3d cluster.
  /// </summary>
  public string? Network { get; set; }

  /// <summary>
  /// Subnet to use for the K3d cluster.
  /// </summary>
  public string? Subnet { get; set; }

  /// <summary>
  /// cancellationToken to use for the K3d cluster.
  /// </summary>
  public string? Token { get; set; }

  /// <summary>
  /// Volumes to use for the K3d cluster.
  /// </summary>
  public IEnumerable<K3dVolume>? Volumes { get; set; }

  /// <summary>
  /// Ports to use for the K3d cluster.
  /// </summary>
  public IEnumerable<K3dPort>? Ports { get; set; }

  /// <summary>
  /// Environment variables to use for the K3d cluster.
  /// </summary>
  public IEnumerable<K3dEnv>? Env { get; set; }

  /// <summary>
  /// Files to put into the K3d cluster.
  /// </summary>
  public IEnumerable<K3dFile>? Files { get; set; }

  /// <summary>
  /// Registries to use for the K3d cluster.
  /// </summary>
  public K3dRegistries? Registries { get; set; }

  /// <summary>
  /// /etc/hosts style entries to be injected into /etc/hosts in the node containers and in the NodeHosts section in CoreDNS
  /// </summary>
  public IEnumerable<K3dHostAlias>? HostAliases { get; set; }
  /// <summary>
  /// Options to use for the K3d cluster.
  /// </summary>
  public K3dOptions? Options { get; set; }
}
