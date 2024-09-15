using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.EventGeneratorTests;


/// <summary>
/// Tests for the <see cref="EventGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Event object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidEvent()
  {
    // Arrange
    var generator = new EventGenerator();
    var model = new Eventsv1Event
    {
      ApiVersion = "events.k8s.io/v1",
      Kind = "Event",
      Metadata = new V1ObjectMeta
      {
        Name = "event",
        NamespaceProperty = "default"
      },
      Type = "Warning",
      Reason = "not sure if this what you want to do",
    };

    // Act
    string fileName = "event.yaml";
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
