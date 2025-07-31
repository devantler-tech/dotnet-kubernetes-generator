
namespace DevantlerTech.KubernetesGenerator.Native.Tests.HorizontalPodAutoscalerGeneratorTests;


/// <summary>
/// Tests for the <see cref="HorizontalPodAutoscalerGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated HorizontalPodAutoscaler object with full configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithFullConfiguration_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscaler
    {
      Metadata = new Metadata
      {
        Name = "horizontal-pod-autoscaler",
        Namespace = "default"
      },
      Spec = new HorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new HorizontalPodAutoscalerScaleTargetRef
        {
          Kind = HorizontalPodAutoscalerTargetKind.Deployment,
          Name = "deployment-name"
        },
        MinReplicas = 1,
        MaxReplicas = 10
      }
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
  /// Verifies the generated HorizontalPodAutoscaler object with minimal required properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalConfiguration_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscaler
    {
      Metadata = new Metadata
      {
        Name = "minimal-hpa"
      },
      Spec = new HorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new HorizontalPodAutoscalerScaleTargetRef
        {
          Kind = HorizontalPodAutoscalerTargetKind.ReplicaSet,
          Name = "replicaset-name"
        },
        MaxReplicas = 5
      }
    };

    // Act
    string fileName = "minimal-hpa.yaml";
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

