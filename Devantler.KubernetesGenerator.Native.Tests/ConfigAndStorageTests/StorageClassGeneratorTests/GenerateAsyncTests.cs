using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.StorageClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="StorageClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated StorageClass object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStorageClass()
  {
    // Arrange
    var generator = new StorageClassGenerator();
    var model = new V1StorageClass
    {
      ApiVersion = "storage.k8s.io/v1",
      Kind = "StorageClass",
      Metadata = new V1ObjectMeta
      {
        Name = "storage-class",
        NamespaceProperty = "default"
      },
      Provisioner = "kubernetes.io/aws-ebs",
      Parameters = new Dictionary<string, string>
      {
        ["type"] = "gp2"
      },
      ReclaimPolicy = "Retain",
      MountOptions = ["option"],
      AllowVolumeExpansion = true,
      VolumeBindingMode = "Immediate"
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "storage-class.yaml");
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
