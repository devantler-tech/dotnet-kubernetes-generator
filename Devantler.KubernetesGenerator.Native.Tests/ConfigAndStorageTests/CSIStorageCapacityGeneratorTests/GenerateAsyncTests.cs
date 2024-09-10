using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.CSIStorageCapacityGeneratorTests;


/// <summary>
/// Tests for the <see cref="CSIStorageCapacityGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CSIStorageCapacity object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCSIStorageCapacity()
  {
    // Arrange
    var generator = new CSIStorageCapacityGenerator();
    var model = new V1CSIStorageCapacity
    {
      ApiVersion = "v1",
      Kind = "CSIStorageCapacity",
      Metadata = new V1ObjectMeta
      {
        Name = "csi-storage-capacity",
        NamespaceProperty = "default"
      },
      Capacity = new ResourceQuantity("1Gi"),
      MaximumVolumeSize = new ResourceQuantity("1Gi"),
      StorageClassName = "storage-class",
      NodeTopology = new V1LabelSelector
      {
        MatchLabels = new Dictionary<string, string>
        {
          ["key"] = "value"
        }
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "csi-storage-capacity.yaml");
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
