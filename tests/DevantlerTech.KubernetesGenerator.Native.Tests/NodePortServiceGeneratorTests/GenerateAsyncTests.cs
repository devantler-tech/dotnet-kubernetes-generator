using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.NodePortServiceGeneratorTests;

/// <summary>
/// Tests for the <see cref="NodePortServiceGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NodePort Service object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNodePortService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new NodePortServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "nodeport-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        Ports =
        [
          new V1ServicePort
          {
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080,
            NodePort = 30080
          }
        ],
        Type = "NodePort"
      }
    };

    // Act
    string fileName = "nodeport-service.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithServiceWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new NodePortServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName())).ConfigureAwait(false);
  }
}
