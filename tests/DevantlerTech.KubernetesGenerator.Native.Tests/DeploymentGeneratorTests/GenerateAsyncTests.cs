using System.Collections.ObjectModel;
using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DeploymentGeneratorTests;


/// <summary>
/// Tests for the <see cref="DeploymentGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Deployment object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = "deployment",
        NamespaceProperty = "default"
      },
      Images = new Collection<string>(["nginx"]),
      Replicas = 1,
      Port = 80
    };

    // Act
    string fileName = "deployment.yaml";
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
  /// Verifies the generated Deployment object with minimal required fields.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = "deployment-minimal"
      },
      Images = new Collection<string>(["nginx"])
    };

    // Act
    string fileName = "deployment-minimal.yaml";
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
  /// Verifies the generated Deployment object with multiple images.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleImages_ShouldGenerateAValidDeployment()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = "deployment-multi-image",
        NamespaceProperty = "default"
      },
      Images = new Collection<string>(["nginx", "redis", "postgres"]),
      Replicas = 3,
      Port = 8080
    };

    // Act
    string fileName = "deployment-multi-image.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the Deployment model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      Images = new Collection<string>(["nginx"])
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the Deployment model does not have any images.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutImages_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = "deployment"
      },
      Images = []
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the Deployment model has null or empty images.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullOrEmptyImage_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DeploymentGenerator();
    var model = new KubernetesDeployment
    {
      Metadata = new V1ObjectMeta
      {
        Name = "deployment"
      },
      Images = new Collection<string>(["nginx", "", "redis"])
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

