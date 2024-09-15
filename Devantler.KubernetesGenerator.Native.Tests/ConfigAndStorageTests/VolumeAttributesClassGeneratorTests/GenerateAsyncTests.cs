using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.VolumeAttributesClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="VolumeAttributesClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated VolumeAttributesClass object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidVolumeAttributesClass()
  {
    // Arrange
    var generator = new VolumeAttributesClassGenerator();
    var model = new V1beta1VolumeAttributesClass
    {
      ApiVersion = "storage.k8s.io/v1",
      Kind = "VolumeAttributesClass",
      Metadata = new V1ObjectMeta
      {
        Name = "volume-attributes-class",
        NamespaceProperty = "default"
      },
      DriverName = "driver",
      Parameters = new Dictionary<string, string>
      {
        ["key"] = "value"
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "volume-attributes-class.yaml");
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
