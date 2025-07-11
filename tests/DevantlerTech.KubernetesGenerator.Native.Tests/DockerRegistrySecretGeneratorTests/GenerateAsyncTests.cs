using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.DockerRegistrySecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="DockerRegistrySecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Docker registry Secret object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecret_ShouldGenerateAValidDockerRegistrySecret()
  {
    // Arrange
    var generator = new DockerRegistrySecretGenerator();
    var model = new DockerRegistrySecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "docker-registry-secret",
        NamespaceProperty = "default"
      },
      DockerServer = "https://index.docker.io/v1/",
      DockerUsername = "myuser",
      DockerPassword = "mypassword",
      DockerEmail = "myuser@example.com"
    };

    // Act
    string fileName = "docker-registry-secret.yaml";
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
  /// Verifies the generated Docker registry Secret object with minimal required fields.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecretMinimal_ShouldGenerateAValidDockerRegistrySecret()
  {
    // Arrange
    var generator = new DockerRegistrySecretGenerator();
    var model = new DockerRegistrySecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "docker-registry-secret-minimal"
      },
      DockerUsername = "user",
      DockerPassword = "pass",
      DockerEmail = "user@example.com"
    };

    // Act
    string fileName = "docker-registry-secret-minimal.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the DockerRegistrySecret model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecretWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new DockerRegistrySecretGenerator();

    var model = new DockerRegistrySecret
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      DockerEmail = "myuser@example.com",
      DockerUsername = "myuser",
      DockerPassword = "mypassword"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
