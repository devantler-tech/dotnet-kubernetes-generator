using Devantler.KubernetesGenerator.Native.Service;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ServiceTests.IngressClassGeneratorTests;


/// <summary>
/// Tests for the <see cref="IngressClassGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Endpoint object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidEndpoint()
  {
    // Arrange
    var generator = new IngressClassGenerator();
    var model = new V1IngressClass
    {
      ApiVersion = "networking.k8s.io/v1",
      Kind = "IngressClass",
      Metadata = new V1ObjectMeta
      {
        Name = "ingress-class",
        NamespaceProperty = "default"
      },
      Spec = new V1IngressClassSpec
      {
        Controller = "nginx"
      }
    };

    // Act
    string fileName = "ingress-class.yaml";
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

