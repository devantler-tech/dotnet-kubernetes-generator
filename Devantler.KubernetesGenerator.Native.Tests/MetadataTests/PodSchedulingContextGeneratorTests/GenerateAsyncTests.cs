using Devantler.KubernetesGenerator.Native.Metadata;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.MetadataTests.PodSchedulingContextGeneratorTests;


/// <summary>
/// Tests for the <see cref="PodSchedulingContextGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated PodSchedulingContext object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidPodSchedulingContext()
  {
    // Arrange
    var generator = new PodSchedulingContextGenerator();
    var model = new V1alpha3PodSchedulingContext
    {
      ApiVersion = "resource.k8s.io/v1alpha3",
      Kind = "PodSchedulingContext",
      Metadata = new V1ObjectMeta
      {
        Name = "pod-scheduling-context",
        NamespaceProperty = "default"
      },
      Spec = new V1alpha3PodSchedulingContextSpec
      {
        PotentialNodes =
        [
          "node1",
          "node2"
        ],
        SelectedNode = "node1",
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "pod-scheduling-context.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}

