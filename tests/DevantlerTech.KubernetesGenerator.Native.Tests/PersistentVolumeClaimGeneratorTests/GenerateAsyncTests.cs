using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PersistentVolumeClaimGeneratorTests;


/// <summary>
/// Tests for the <see cref="PersistentVolumeClaimGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PersistentVolumeClaim object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new V1ObjectMeta
      {
        Name = "persistent-volume-claim",
        NamespaceProperty = "default"
      },
      AccessModes = ["ReadWriteOnce"],
      StorageSize = "1Gi",
      StorageClassName = "storage-class",
      VolumeMode = "Filesystem",
      VolumeName = "volume-name",
      Selector = new V1LabelSelector
      {
        MatchLabels = new Dictionary<string, string>
        {
          ["key"] = "value"
        }
      },
      DataSource = new V1TypedLocalObjectReference
      {
        ApiGroup = "storage.k8s.io",
        Kind = "StorageClass",
        Name = "storage-class"
      },
      DataSourceRef = new V1TypedObjectReference
      {
        ApiGroup = "storage.k8s.io",
        Kind = "PersistentVolumeClaim",
        Name = "pvc",
        NamespaceProperty = "default"
      }
    };

    // Act
    string fileName = "persistent-volume-claim.yaml";
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

  /// <summary>
  /// Verifies the generated PersistentVolumeClaim object with minimal required fields.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalRequiredFields_ShouldGenerateAValidPersistentVolumeClaim()
  {
    // Arrange
    var generator = new PersistentVolumeClaimGenerator();
    var model = new PersistentVolumeClaim
    {
      Metadata = new V1ObjectMeta
      {
        Name = "minimal-pvc"
      },
      AccessModes = ["ReadWriteOnce"],
      StorageSize = "2Gi"
    };

    // Act
    string fileName = "minimal-persistent-volume-claim.yaml";
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
