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
  /// Verifies the generated HorizontalPodAutoscaler object with StatefulSet target.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithStatefulSetTarget_ShouldGenerateAValidHorizontalPodAutoscaler()
  {
    // Arrange
    var generator = new HorizontalPodAutoscalerGenerator();
    var model = new HorizontalPodAutoscaler
    {
      Metadata = new Metadata
      {
        Name = "hpa-statefulset",
        Namespace = "testing"
      },
      Spec = new HorizontalPodAutoscalerSpec
      {
        ScaleTargetRef = new HorizontalPodAutoscalerScaleTargetRef
        {
          Kind = HorizontalPodAutoscalerTargetKind.StatefulSet,
          Name = "statefulset-name"
        },
        MinReplicas = 2,
        MaxReplicas = 20
      }
    };

    // Act
    string fileName = "hpa-statefulset.yaml";
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
  /// Verifies the generated HorizontalPodAutoscaler object with minimal properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidHorizontalPodAutoscaler()
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

