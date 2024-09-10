using System.Text;
using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.StorageVersionMigrationGeneratorTests;


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
      ApiVersion = "storage.k8s.io/v1alpha1",
      Kind = "StorageVersionMigration",
      Metadata = new V1ObjectMeta
      {
        Name = "storage-version-migration",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha1StorageVersionMigrationSpec
      {
        ContinueToken = "token",
        Resource = new V1alpha1GroupVersionResource
        {
          Group = "group",
          Resource = "resource",
          Version = "version"
        },
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "storage-version-migration.yaml");
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
