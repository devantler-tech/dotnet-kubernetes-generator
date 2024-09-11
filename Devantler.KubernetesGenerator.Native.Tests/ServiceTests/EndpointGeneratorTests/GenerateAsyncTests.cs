using Devantler.KubernetesGenerator.Native.Service;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ServiceTests.EndpointGeneratorTests;


/// <summary>
/// Tests for the <see cref="EndpointsGenerator"/> class.
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
    var generator = new EndpointsGenerator();
    var model = new V1Endpoints
    {
      ApiVersion = "v1",
      Kind = "Endpoints",
      Metadata = new V1ObjectMeta
      {
        Name = "endpoints",
        NamespaceProperty = "default"
      },
      Subsets =
      [
        new V1EndpointSubset
        {
          Addresses =
          [
            new V1EndpointAddress
            {
              Hostname = "hostname",
              Ip = "192.1.1.4",
              NodeName = "node-name",
              TargetRef = new V1ObjectReference
              {
                ApiVersion = "v1",
                Kind = "Pod",
                Name = "pod-name",
                NamespaceProperty = "default",
                ResourceVersion = "1",
              }
            }
          ]
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "endpoints.yaml");
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

