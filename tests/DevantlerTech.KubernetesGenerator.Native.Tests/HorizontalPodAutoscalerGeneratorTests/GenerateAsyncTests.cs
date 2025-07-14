using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.HorizontalPodAutoscalerGeneratorTests;


/// <summary>
/// Tests for the <see cref="HorizontalPodAutoscalerGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated HorizontalPodAutoscaler object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscaler("horizontal-pod-autoscaler")
    {
      ApiVersion = "autoscaling/v2",
      Kind = "HorizontalPodAutoscaler",
      Metadata = new Metadata
      {
        Name = "horizontal-pod-autoscaler",
        Namespace = "default"
      },
      Spec = new HorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new ScaleTargetRef
        {
          ApiVersion = "apps/v1",
          Kind = "Deployment",
          Name = "deployment-name"
        },
        MinReplicas = 1,
        MaxReplicas = 10,
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
}

