using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.RuntimeClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="RuntimeClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated RuntimeClass object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidRuntimeClass()
  {
    // Arrange
    var generator = new RuntimeClassGenerator();
    var model = new V1RuntimeClass
    {
      ApiVersion = "node.k8s.io/v1",
      Kind = "RuntimeClass",
      Metadata = new V1ObjectMeta
      {
        Name = "runtime-class",
        NamespaceProperty = "default"
      },
      Handler = "handler",
      Overhead = new V1Overhead
      {
        PodFixed = new Dictionary<string, ResourceQuantity>
        {
          ["key"] = new ResourceQuantity("1")
        }
      },
      Scheduling = new V1Scheduling
      {
        NodeSelector = new Dictionary<string, string>
        {
          ["key"] = "value"
        },
        Tolerations =
        [
          new V1Toleration
          {
            Effect = "NoSchedule",
            Key = "key",
            OperatorProperty = "Exists",
            TolerationSeconds = 10,
            Value = "value"
          }
        ]
      },
    };

    // Act
    string fileName = "runtime-class.yaml";
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
