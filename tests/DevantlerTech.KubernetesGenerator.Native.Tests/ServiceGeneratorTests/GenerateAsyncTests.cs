using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ServiceGeneratorTests;


/// <summary>
/// Tests for the <see cref="ServiceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Endpoint object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidEndpoint()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        ClusterIP = "192.168.34.21",
        Ports =
        [
          new V1ServicePort
          {
            Name = "port-name",
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080
          }
        ],
        Selector = new Dictionary<string, string>
        {
          ["app"] = "app"
        },
        Type = "ClusterIP"
      }
    };

    // Act
    string fileName = "service.yaml";
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

