using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

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
    var model = new DockerRegistrySecret("docker-registry-secret")
    {
      Metadata = { Namespace = "default" },
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
    var model = new DockerRegistrySecret("docker-registry-secret-minimal")
    {
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

}
