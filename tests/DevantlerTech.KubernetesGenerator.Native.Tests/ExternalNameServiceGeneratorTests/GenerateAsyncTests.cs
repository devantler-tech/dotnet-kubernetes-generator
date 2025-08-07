using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ExternalNameServiceGeneratorTests;

/// <summary>
/// Tests for the <see cref="ExternalNameServiceGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ExternalName Service object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithExternalNameService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ExternalNameServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "external-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        Type = "ExternalName",
        ExternalName = "external.example.com"
      }
    };

    // Act
    string fileName = "external-service.yaml";
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
    var generator = new ExternalNameServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when ExternalName service doesn't have ExternalName set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithExternalNameServiceWithoutExternalName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ExternalNameServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "external-service"
      },
      Spec = new V1ServiceSpec
      {
        Type = "ExternalName"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
