using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.GenericSecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="GenericSecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated generic Secret object using GenericSecret input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new GenericSecret("generic-secret")
    {
      Metadata = { Namespace = "default" },
      Type = "Opaque"
    };
    model.Data.Add("key1", "value1");
    model.Data.Add("key2", "value2");

    // Act
    string fileName = "generic-secret.yaml";
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
  /// Verifies the generated generic Secret object without specifying a type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretWithoutType_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new GenericSecret("generic-secret-no-type")
    {
      Metadata = { Namespace = "default" }
    };
    model.Data.Add("key1", "value1");

    // Act
    string fileName = "generic-secret-no-type.yaml";
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
  /// Verifies the generated generic Secret object with mixed data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretWithDataAndStringData_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new GenericSecret("generic-secret-mixed")
    {
      Metadata = { Namespace = "default" },
      Type = "Opaque"
    };
    model.Data.Add("data-key", "data-value");
    model.Data.Add("string-key", "string-value");
    model.Data.Add("override-key", "string-override");

    // Act
    string fileName = "generic-secret-mixed.yaml";
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
    var generator = new GenericSecretGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateAsync(null!, Path.GetTempFileName()));
  }
}
