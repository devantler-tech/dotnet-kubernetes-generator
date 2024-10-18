using Devantler.KubernetesGenerator.Kind.Models;
using Devantler.KubernetesGenerator.Kind.Models.Networking;
using Devantler.KubernetesGenerator.Kind.Models.Nodes;

namespace Devantler.KubernetesGenerator.Kind.Tests.KindConfigGeneratorTests;

/// <summary>
/// Tests for <see cref="KindConfigKubernetesGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  KindConfigGenerator KindConfigKubernetesGenerator { get; } = new();

  /// <summary>
  /// Tests that <see cref="KindConfigKubernetesGenerator"/> generates a valid Kind cluster configuration with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKindConfigFile()
  {
    // Arrange
    var config = new KindConfig
    {
      Name = "kind-default",
      FeatureGates = new KindFeatureGates
      {
        CSIMigration = false
      },
      RuntimeConfig = new Dictionary<string, string>
      {
        { "api/alpha", "false" }
      },
      Networking = new KindNetworking
      {
        IPFamily = KindNetworkingIPFamily.IPv4,
        ApiServerAddress = "127.0.0.1",
        ApiServerPort = 6443,
        PodSubnet = "10.244.0.0/16",
        ServiceSubnet = "10.96.0.0/16",
        DisableDefaultCNI = false,
        KubeProxyMode = KindNetworkingKubeProxyMode.IPTables
      },
      Nodes = [
        new KindNode {
          Role = KindNodeRole.ControlPlane,
          Image = "kindest/node:v1.31.0",
          ExtraMounts = [],
          ExtraPortMappings = [],
          KindNodeLabels = {},
          KubeadmConfigPatches = ""
        },
        new KindNode {
          Role = KindNodeRole.Worker,
          Image = "kindest/node:v1.31.0",
          ExtraMounts = [],
          ExtraPortMappings = [],
          KindNodeLabels = {},
          KubeadmConfigPatches = ""
        }
      ],
      ContainerdConfigPatches = [
        """
        [plugins."io.containerd.grpc.v1.cri".registry]
          config_path = "/etc/containerd/certs.d"
        """
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "kind-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await KindConfigKubernetesGenerator.GenerateAsync(config, outputPath);
    string kindConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kindConfigFromFile, extension: "yaml").UseFileName("kind-config.full");

    // Cleanup
    File.Delete(outputPath);
  }


  /// <summary>
  /// Tests that <see cref="KindConfigKubernetesGenerator"/> generates a valid Kind cluster configuration with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidKindConfigFile()
  {
    // Arrange
    var config = new KindConfig();

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "kind-config.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await KindConfigKubernetesGenerator.GenerateAsync(config, outputPath, true);
    string kindConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(kindConfigFromFile, extension: "yaml").UseFileName("kind-config.minimal");

    // Cleanup
    File.Delete(outputPath);
  }
}
