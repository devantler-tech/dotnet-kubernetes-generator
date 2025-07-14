using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NamespaceGeneratorTests;

/// <summary>
/// Tests for the <see cref="NamespaceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Namespace object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new Namespace("test-namespace");

    // Act
    string fileName = "namespace.yaml";
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
  /// Verifies the generated Namespace object with save-config enabled.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithSaveConfig_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new Namespace("namespace-with-save-config")
    {
      SaveConfig = true
    };

    // Act
    string fileName = "namespace-with-save-config.yaml";
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
  /// Verifies the generated Namespace object with field manager.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithFieldManager_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new Namespace("namespace-with-field-manager")
    {
      FieldManager = "custom-manager"
    };

    // Act
    string fileName = "namespace-with-field-manager.yaml";
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
  /// Verifies the generated Namespace object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllProperties_ShouldGenerateAValidNamespace()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new Namespace("namespace-with-all-properties")
    {
      SaveConfig = true,
      FieldManager = "test-manager"
    };

    // Act
    string fileName = "namespace-with-all-properties.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NamespaceGenerator();
    var model = new Namespace("test")
    {
      Metadata = { Name = "" } // Empty name
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
