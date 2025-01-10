using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.PriorityClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="PriorityClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PriorityClass object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new V1PriorityClass
    {
      ApiVersion = "scheduling.k8s.io/v1",
      Kind = "PriorityClass",
      Metadata = new V1ObjectMeta
      {
        Name = "priority-class",
        NamespaceProperty = "default"
      },
      Description = "PriorityClass for high-priority pods",
      GlobalDefault = false,
      Value = 1000,
      PreemptionPolicy = "Never"
    };

    // Act
    string fileName = "priority-class.yaml";
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

