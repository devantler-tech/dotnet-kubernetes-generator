using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;

/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ConfigMap object with literal data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralData_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap("config-map")
    {
      Metadata = { Namespace = "default" }
    };
    model.Data.Add("key", "value");

    // Act
    string fileName = "config-map.yaml";
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
  /// Verifies that a <see cref="ArgumentNullException"/> is thrown when the model is null.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new ConfigMapGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateAsync(null!, Path.GetTempFileName()));
  }
}
