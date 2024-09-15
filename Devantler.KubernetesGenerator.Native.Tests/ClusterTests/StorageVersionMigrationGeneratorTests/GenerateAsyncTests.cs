using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.StorageVersionMigrationGeneratorTests;


/// <summary>
/// Tests for the <see cref="StorageVersionMigrationGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated StorageVersionMigration object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidStorageVersionMigration()
  {
    // Arrange
    var generator = new StorageVersionMigrationGenerator();
    var model = new V1alpha1StorageVersionMigration
    {
      ApiVersion = "storagemigration.k8s.io/v1alpha1",
      Kind = "StorageVersionMigration",
      Metadata = new V1ObjectMeta
      {
        Name = "storage-version-migration",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha1StorageVersionMigrationSpec
      {
        ContinueToken = "continue-token",
        Resource = new V1alpha1GroupVersionResource
        {
          Group = "group",
          Version = "version",
          Resource = "resource"
        }
      }
    };

    // Act
    string fileName = "storage-version-migration.yaml";
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
