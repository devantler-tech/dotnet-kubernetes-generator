
namespace DevantlerTech.KubernetesGenerator.Native.Tests.HorizontalPodAutoscalerGeneratorTests;


/// <summary>
/// Tests for the <see cref="HorizontalPodAutoscalerGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeHorizontalPodAutoscaler object with full configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithFullConfiguration_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new NativeHorizontalPodAutoscaler
    {
      Metadata = new Metadata
      {
        Name = "horizontal-pod-autoscaler",
        Namespace = "default"
      },
      Spec = new NativeHorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new NativeHorizontalPodAutoscalerScaleTargetRef
        {
          Kind = NativeHorizontalPodAutoscalerTargetKind.Deployment,
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
  /// Verifies the generated NativeHorizontalPodAutoscaler object with minimal required properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalConfiguration_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new NativeHorizontalPodAutoscaler
    {
      Metadata = new Metadata
      {
        Name = "minimal-hpa"
      },
      Spec = new NativeHorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new NativeHorizontalPodAutoscalerScaleTargetRef
        {
          Kind = NativeHorizontalPodAutoscalerTargetKind.ReplicaSet,
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

