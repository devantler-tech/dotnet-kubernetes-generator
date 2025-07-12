using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterIPServiceGeneratorTests;

/// <summary>
/// Tests for the <see cref="ClusterIPServiceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterIP Service object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithClusterIPService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ClusterIPServiceGenerator();
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

  /// <summary>
  /// Verifies the generated Service object with headless ClusterIP (None).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHeadlessService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ClusterIPServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "headless-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        ClusterIP = "None",
        Ports =
        [
          new V1ServicePort
          {
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080
          }
        ],
        Type = "ClusterIP"
      }
    };

    // Act
    string fileName = "headless-service.yaml";
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
    var generator = new ClusterIPServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}