using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.LoadBalancerServiceGeneratorTests;

/// <summary>
/// Tests for the <see cref="LoadBalancerServiceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated LoadBalancer Service object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLoadBalancerService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new LoadBalancerServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "loadbalancer-service",
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
            TargetPort = 8080
          }
        ],
        Type = "LoadBalancer"
      }
    };

    // Act
    string fileName = "loadbalancer-service.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithServiceWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new LoadBalancerServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}