using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.HorizontalPodAutoscalerGeneratorTests;


/// <summary>
/// Tests for the <see cref="HorizontalPodAutoscalerGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated HorizontalPodAutoscaler object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscalerCreateOptions
    {
      Metadata = new HorizontalPodAutoscalerMetadata
      {
        Name = "horizontal-pod-autoscaler",
        Namespace = "default"
      },
      ScaleTargetRef = new ScaleTargetRef
      {
        ApiVersion = "apps/v1",
        Kind = "Deployment",
        Name = "deployment-name"
      },
      MinReplicas = 1,
      MaxReplicas = 10,
      TargetCPUUtilizationPercentage = 80
    };

    // Act
    string fileName = "horizontal-pod-autoscaler.yaml";
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
  /// Verifies the generated HorizontalPodAutoscaler object with minimal properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscalerCreateOptions
    {
      ScaleTargetRef = new ScaleTargetRef
      {
        Kind = "Deployment",
        Name = "my-deployment"
      },
      MaxReplicas = 5
    };

    // Act
    string fileName = "horizontal-pod-autoscaler-minimal.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when ScaleTargetRef is null.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullScaleTargetRef_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscalerCreateOptions
    {
      ScaleTargetRef = null!,
      MaxReplicas = 5
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when ScaleTargetRef.Kind is empty.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithEmptyScaleTargetRefKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscalerCreateOptions
    {
      ScaleTargetRef = new ScaleTargetRef
      {
        Kind = "",
        Name = "my-deployment"
      },
      MaxReplicas = 5
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when ScaleTargetRef.Name is empty.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithEmptyScaleTargetRefName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscalerCreateOptions
    {
      ScaleTargetRef = new ScaleTargetRef
      {
        Kind = "Deployment",
        Name = ""
      },
      MaxReplicas = 5
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

