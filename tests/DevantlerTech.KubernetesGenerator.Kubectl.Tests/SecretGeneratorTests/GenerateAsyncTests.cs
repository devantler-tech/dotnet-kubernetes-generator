using DevantlerTech.KubernetesGenerator.Kubectl.Models;

namespace DevantlerTech.KubernetesGenerator.Kubectl.Tests.SecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="SecretGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated generic secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new KubectlGenericSecret
    {
      Name = "my-secret",
      Namespace = "default",
      Type = "Opaque",
      FromLiteral = new Dictionary<string, string>
      {
        ["key1"] = "value1",
        ["key2"] = "value2"
      }
    };

    // Act
    string fileName = "generic-secret.yaml";
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
  /// Verifies the generated docker-registry secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new KubectlDockerRegistrySecret
    {
      Name = "my-docker-secret",
      Namespace = "default",
      DockerServer = "registry.example.com",
      DockerUsername = "user",
      DockerPassword = "pass",
      DockerEmail = "user@example.com"
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
  /// Verifies the generated TLS secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithTlsSecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new KubectlTlsSecret
    {
      Name = "my-tls-secret",
      Namespace = "default",
      CertPath = "/tmp/test-certs/test.crt",
      KeyPath = "/tmp/test-certs/test.key"
    };

    // Act
    string fileName = "tls-secret.yaml";
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