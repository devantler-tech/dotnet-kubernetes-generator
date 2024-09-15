using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.StorageVersionGeneratorTests;


/// <summary>
/// Tests for the <see cref="StorageVersionGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated StorageVersion object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStorageVersion()
  {
    // Arrange
    var generator = new StorageVersionGenerator();
    var model = new V1alpha1StorageVersion
    {
      ApiVersion = "internal.apiserver.k8s.io/v1alpha1",
      Kind = "StorageVersion",
      Metadata = new V1ObjectMeta
      {
        Name = "storage-version",
        NamespaceProperty = "default"
      },
      Spec = new
      {
        Test = "test"
      }
    };

    // Act
    string fileName = "storage-version.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}
