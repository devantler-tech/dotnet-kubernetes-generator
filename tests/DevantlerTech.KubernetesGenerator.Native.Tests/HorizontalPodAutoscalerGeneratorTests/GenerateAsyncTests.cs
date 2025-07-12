using k8s.Models;

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
    var model = new V2HorizontalPodAutoscaler
    {
      ApiVersion = "autoscaling/v2",
      Kind = "HorizontalPodAutoscaler",
      Metadata = new V1ObjectMeta
      {
        Name = "horizontal-pod-autoscaler",
        NamespaceProperty = "default"
      },
      Spec = new V2HorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new V2CrossVersionObjectReference
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

