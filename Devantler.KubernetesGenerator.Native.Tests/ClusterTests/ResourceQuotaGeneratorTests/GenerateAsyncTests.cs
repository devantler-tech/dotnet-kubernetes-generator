using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.ResourceQuotaGeneratorTests;


/// <summary>
/// Tests for the <see cref="ResourceQuotaGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ResourceQuota object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidResourceQuota()
  {
    // Arrange
    var generator = new ResourceQuotaGenerator();
    var model = new V1ResourceQuota
    {
      ApiVersion = "v1",
      Kind = "ResourceQuota",
      Metadata = new V1ObjectMeta
      {
        Name = "resource-quota",
        NamespaceProperty = "default"
      },
      Spec = new V1ResourceQuotaSpec
      {
        Hard = new Dictionary<string, ResourceQuantity>
        {
          ["requests.cpu"] = new ResourceQuantity("1"),
          ["requests.memory"] = new ResourceQuantity("1Gi"),
          ["limits.cpu"] = new ResourceQuantity("1"),
          ["limits.memory"] = new ResourceQuantity("1Gi")
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "resource-quota.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}
