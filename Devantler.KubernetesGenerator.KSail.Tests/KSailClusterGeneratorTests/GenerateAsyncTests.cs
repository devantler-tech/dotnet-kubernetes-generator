using Devantler.KubernetesGenerator.KSail.Models;
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
        Distribution = KSailKubernetesDistribution.K3d
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
    _ = await Verify(ksailClusterConfigFromFile);

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
    _ = await Verify(ksailClusterConfigFromFile);

    // Cleanup
    File.Delete(outputPath);
  }
}
