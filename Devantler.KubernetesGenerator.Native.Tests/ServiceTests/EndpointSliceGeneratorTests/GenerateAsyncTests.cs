using Devantler.KubernetesGenerator.Native.Service;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ServiceTests.EndpointSliceGeneratorTests;


/// <summary>
/// Tests for the <see cref="EndpointSliceGenerator"/> class.
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
    var generator = new EndpointSliceGenerator();
    var model = new V1EndpointSlice
    {
      ApiVersion = "discovery.k8s.io/v1",
      Kind = "EndpointSlice",
      Metadata = new V1ObjectMeta
      {
        Name = "endpoint-slice",
        NamespaceProperty = "default"
      },
      AddressType = "IPv4",
      Endpoints =
      [
        new V1Endpoint
        {
          Addresses = ["192.168.32.4"],
        }
      ],
      Ports =
      [
        new Discoveryv1EndpointPort
        {
          Name = "port-name",
          Port = 80,
          Protocol = "TCP"
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "endpoint-slice.yaml");
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

