using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.CSINodeGeneratorTests;


/// <summary>
/// Tests for the <see cref="CSINodeGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CSINode object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCSINode()
  {
    // Arrange
    var generator = new CSINodeGenerator();
    var model = new V1CSINode
    {
      ApiVersion = "storage.k8s.io/v1",
      Kind = "CSINode",
      Metadata = new V1ObjectMeta
      {
        Name = "csi-node",
        NamespaceProperty = "default"
      },
      Spec = new V1CSINodeSpec
      {
        Drivers =
        [
          new V1CSINodeDriver
          {
            Name = "csi-driver",
            NodeID = "node-id",
            TopologyKeys =
            [
              "key"
            ],
            Allocatable = new V1VolumeNodeResources
            {
              Count = 1
            }
          }
        ]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "csi-node.yaml");
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
