using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PriorityClassGeneratorTests;

/// <summary>
/// Tests for the <see cref="PriorityClassGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PriorityClass object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new PriorityClass
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "high-priority"
      },
      Value = 1000,
      Description = "PriorityClass for high-priority pods",
      GlobalDefault = false,
      PreemptionPolicy = PreemptionPolicy.Never
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

  /// <summary>
  /// Verifies the generated PriorityClass object with only required properties (name and value).
  /// This test validates that kubectl applies correct defaults for optional properties like preemptionPolicy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithOnlyRequiredProperties_ShouldApplyCorrectDefaults()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new PriorityClass
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "basic-priority"
      },
      Value = 500
    };

    // Act
    string fileName = "priority-class-required-only.yaml";
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
  /// Verifies the generated PriorityClass object with global default set to true.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGlobalDefault_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new PriorityClass
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "default-priority"
      },
      Value = 100,
      Description = "Default priority class",
      GlobalDefault = true,
      PreemptionPolicy = PreemptionPolicy.PreemptLowerPriority
    };

    // Act
    string fileName = "priority-class-global-default.yaml";
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

