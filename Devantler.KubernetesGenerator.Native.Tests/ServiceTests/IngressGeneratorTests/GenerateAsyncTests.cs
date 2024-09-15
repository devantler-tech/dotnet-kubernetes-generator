using Devantler.KubernetesGenerator.Native.Service;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ServiceTests.IngressGeneratorTests;


/// <summary>
/// Tests for the <see cref="IngressGenerator"/> class.
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
    var generator = new IngressGenerator();
    var model = new V1Ingress
    {
      ApiVersion = "networking.k8s.io/v1",
      Kind = "Ingress",
      Metadata = new V1ObjectMeta
      {
        Name = "ingress",
        NamespaceProperty = "default"
      },
      Spec = new V1IngressSpec
      {
        DefaultBackend = new V1IngressBackend
        {
          Service = new V1IngressServiceBackend
          {
            Name = "service-name",
            Port = new V1ServiceBackendPort
            {
              Number = 80
            }
          }
        },
        Rules =
        [
          new V1IngressRule
          {
            Host = "host",
            Http = new V1HTTPIngressRuleValue
            {
              Paths =
              [
                new V1HTTPIngressPath
                {
                  Path = "/path",
                  Backend = new V1IngressBackend
                  {
                    Service = new V1IngressServiceBackend
                    {
                      Name = "service-name",
                      Port = new V1ServiceBackendPort
                      {
                        Number = 80
                      }
                    }
                  }
                }
              ]
            }
          }
        ]
      }
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "ingress.yaml");
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

