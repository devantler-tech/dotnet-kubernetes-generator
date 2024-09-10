using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.ComponentStatusGeneratorTests;


/// <summary>
/// Tests for the <see cref="ComponentStatusGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ComponentStatus object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidComponentStatus()
  {
    // Arrange
    var generator = new ComponentStatusGenerator();
    var model = new V1ComponentStatus
    {
      ApiVersion = "v1",
      Kind = "ComponentStatus",
      Metadata = new V1ObjectMeta
      {
        Name = "component-status",
        NamespaceProperty = "default"
      },
      Conditions =
      [
        new V1ComponentCondition
        {
          Status = "True",
          Type = "Healthy"
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "component-status.yaml");
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
