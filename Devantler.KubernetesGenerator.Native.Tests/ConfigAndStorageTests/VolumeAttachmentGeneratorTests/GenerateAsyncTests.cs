using Devantler.KubernetesGenerator.Native.ConfigAndStorage;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ConfigAndStorageTests.VolumeAttachmentGeneratorTests;


/// <summary>
/// Tests for the <see cref="VolumeAttachmentGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated VolumeAttachment object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidVolumeAttachment()
  {
    // Arrange
    var generator = new VolumeAttachmentGenerator();
    var model = new V1VolumeAttachment
    {
      ApiVersion = "storage.k8s.io/v1",
      Kind = "VolumeAttachment",
      Metadata = new V1ObjectMeta
      {
        Name = "volume-attachment",
        NamespaceProperty = "default"
      },
      Spec = new V1VolumeAttachmentSpec
      {
        Attacher = "attacher",
        NodeName = "node",
        Source = new V1VolumeAttachmentSource
        {
          PersistentVolumeName = "persistent-volume"
        }
      }
    };

    // Act
    string fileName = "volume-attachment.yaml";
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
