using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.ControllerRevisionGeneratorTests;


/// <summary>
/// Tests for the <see cref="ControllerRevisionGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ControllerRevision object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidControllerRevision()
  {
    // Arrange
    var generator = new ControllerRevisionGenerator();
    var model = new V1ControllerRevision
    {
      ApiVersion = "apps/v1",
      Kind = "ControllerRevision",
      Metadata = new V1ObjectMeta
      {
        Name = "controller-revision",
        NamespaceProperty = "default"
      },
      Data = new
      {
        Test = "test"
      },
      Revision = 1,
    };

    // Act
    string fileName = "controller-revision.yaml";
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
