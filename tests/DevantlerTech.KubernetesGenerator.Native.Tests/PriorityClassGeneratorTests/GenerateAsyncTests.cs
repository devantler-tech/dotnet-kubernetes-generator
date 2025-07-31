using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.PriorityClassGeneratorTests;

/// <summary>
/// Tests for the <see cref="PriorityClassGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativePriorityClass object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = "high-priority"
      },
      Value = 1000,
      Description = "NativePriorityClass for high-priority pods",
      GlobalDefault = false,
      PreemptionPolicy = NativePreemptionPolicy.Never
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
  /// Verifies the generated NativePriorityClass object with only required properties (name and value).
  /// This test validates that kubectl applies correct defaults for optional properties like preemptionPolicy.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithOnlyRequiredProperties_ShouldApplyCorrectDefaults()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
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
  /// Verifies the generated NativePriorityClass object with global default set to true.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGlobalDefault_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = "default-priority"
      },
      Value = 100,
      Description = "Default priority class",
      GlobalDefault = true,
      PreemptionPolicy = NativePreemptionPolicy.PreemptLowerPriority
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

  /// <summary>
  /// Verifies that ArgumentNullException is thrown when model is null.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    NativePriorityClass? model = null;

    // Act & Assert
    var exception = await Assert.ThrowsAsync<ArgumentNullException>(
      () => generator.GenerateAsync(model!, "output.yaml"));

    Assert.Equal("model", exception.ParamName);
  }

  /// <summary>
  /// Verifies that KubernetesGeneratorException is thrown when NativePriorityClass name is null or empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = ""  // Empty name
      },
      Value = 500
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, "output.yaml"));

    Assert.Equal("A non-empty NativePriorityClass name must be provided.", exception.Message);
  }

  /// <summary>
  /// Verifies that KubernetesGeneratorException is thrown when NativePriorityClass name is whitespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithWhitespaceName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = "   "  // Whitespace name
      },
      Value = 500
    };

    // Act & Assert
    var exception = await Assert.ThrowsAsync<KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, "output.yaml"));

    Assert.Equal("A non-empty NativePriorityClass name must be provided.", exception.Message);
  }

  /// <summary>
  /// Verifies the generated NativePriorityClass object with empty description string.
  /// This test ensures that empty description strings are handled correctly by the AddArguments method.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyDescription_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new NativePriorityClass
    {
      Metadata = new NativeClusterScopedMetadata
      {
        Name = "test-priority"
      },
      Value = 300,
      Description = "",  // Empty description to test the AddArguments method
      GlobalDefault = false,
      PreemptionPolicy = NativePreemptionPolicy.PreemptLowerPriority
    };

    // Act
    string fileName = "priority-class-empty-description.yaml";
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

