using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.CSIDriverGeneratorTests;


/// <summary>
/// Tests for the <see cref="CSIDriverGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated CSIDriver object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidCSIDriver()
  {
    // Arrange
    var generator = new CSIDriverGenerator();
    var model = new V1CSIDriver
    {
      ApiVersion = "storage.k8s.io/v1",
      Kind = "CSIDriver",
      Metadata = new V1ObjectMeta
      {
        Name = "csi-driver",
        NamespaceProperty = "default"
      },
      Spec = new V1CSIDriverSpec
      {
        AttachRequired = true,
        PodInfoOnMount = true,
        VolumeLifecycleModes =
        [
          "Ephemeral"
        ],
        StorageCapacity = true,
        FsGroupPolicy = "File",
        RequiresRepublish = true,
        SeLinuxMount = true,
        TokenRequests =
        [
          new Storagev1TokenRequest
          {
            Audience = "audience",
             ExpirationSeconds = 3600,
          }
        ]
      }
    };

    // Act
    string fileName = "csi-driver.yaml";
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
