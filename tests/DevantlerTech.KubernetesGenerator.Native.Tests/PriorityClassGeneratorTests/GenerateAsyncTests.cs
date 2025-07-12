using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

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
    var model = new V1PriorityClass
    {
      ApiVersion = "scheduling.k8s.io/v1",
      Kind = "PriorityClass",
      Metadata = new V1ObjectMeta
      {
        Name = "priority-class"
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

  /// <summary>
  /// Verifies the generated PriorityClass object with globalDefault=true.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGlobalDefaultTrue_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new V1PriorityClass
    {
      ApiVersion = "scheduling.k8s.io/v1",
      Kind = "PriorityClass",
      Metadata = new V1ObjectMeta
      {
        Name = "default-priority-class"
      },
      Description = "Default priority class for all pods",
      GlobalDefault = true,
      Value = 500,
      PreemptionPolicy = "PreemptLowerPriority"
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
  /// Verifies the generated PriorityClass object with minimal properties (only name and value).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidPriorityClass()
  {
    // Arrange
    var generator = new PriorityClassGenerator();
    var model = new V1PriorityClass
    {
      ApiVersion = "scheduling.k8s.io/v1",
      Kind = "PriorityClass",
      Metadata = new V1ObjectMeta
      {
        Name = "minimal-priority-class"
      },
      Value = 100
    };

    // Act
    string fileName = "priority-class-minimal.yaml";
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
    var generator = new PriorityClassGenerator();
    var model = new V1PriorityClass
    {
      ApiVersion = "scheduling.k8s.io/v1",
      Kind = "PriorityClass",
      Value = 1000
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

