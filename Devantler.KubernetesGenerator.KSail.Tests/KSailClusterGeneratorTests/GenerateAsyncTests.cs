using Devantler.KubernetesGenerator.KSail.Models;
using Devantler.KubernetesGenerator.KSail.Models.Registry;
using k8s.Models;

namespace Devantler.KubernetesGenerator.KSail.Tests.KSailClusterGeneratorTests;

/// <summary>
/// Tests for <see cref="KSailClusterGenerator"/>.
/// </summary>
public class GenerateAsyncTests
{
  readonly KSailClusterGenerator _generator = new();
  /// <summary>
  /// Tests that <see cref="KSailClusterGenerator"/> generates a valid KSail Cluster configuration with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidFullKSailClusterFile()
  {
    // Arrange
    var cluster = new KSailCluster
    {
      Metadata = new V1ObjectMeta
      {
        Name = "my-cluster"
      },
      Spec = new KSailClusterSpec
      {
        Distribution = KSailKubernetesDistribution.K3d,
        GitOpsTool = KSailGitOpsTool.Flux,
        Registries =
        [
          new KSailRegistry
          {
            Name = "ksail-registry",
            HostPort = 5000,
            IsGitOpsOCISource = true,
          },
          new KSailRegistry
          {
            Name = "mirror-docker.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://registry-1.docker.io")
            },
            HostPort = 5001
          },
          new KSailRegistry
          {
            Name = "mirror-registry.k8s.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://registry.k8s.io")
            },
            HostPort = 5002
          },
          new KSailRegistry
          {
            Name = "mirror-gcr.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://gcr.io")
            },
            HostPort = 5002
          },
          new KSailRegistry
          {
            Name = "mirror-ghcr.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://ghcr.io")
            },
            HostPort = 5003
          },
          new KSailRegistry
          {
            Name = "mirror-mcr.microsoft.com",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://mcr.microsoft.com")
            },
            HostPort = 5004
          },
          new KSailRegistry
          {
            Name = "mirror-quay.io",
            Proxy = new KSailRegistryProxy
            {
              Url = new Uri("https://quay.io")
            },
            HostPort = 5005
          }
        ]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ksail-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await _generator.GenerateAsync(cluster, outputPath, true);
    string ksailClusterConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(ksailClusterConfigFromFile, extension: "yaml").UseFileName("ksail-config.full.yaml");

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Tests that <see cref="KSailClusterGenerator"/> generates a valid KSail cluster configuration with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalPropertiesSet_ShouldGenerateAValidMinimalKSailClusterFile()
  {
    // Arrange
    var cluster = new KSailCluster();

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ksail-config.yaml");
    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    await _generator.GenerateAsync(cluster, outputPath, true);
    string ksailClusterConfigFromFile = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(ksailClusterConfigFromFile, extension: "yaml").UseFileName("ksail-config.minimal.yaml");

    // Cleanup
    File.Delete(outputPath);
  }
}
