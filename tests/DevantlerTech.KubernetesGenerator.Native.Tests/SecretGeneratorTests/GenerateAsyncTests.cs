using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.SecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="SecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Secret object using V1Secret input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "secret",
        NamespaceProperty = "default"
      },
      Type = "Opaque",
      StringData = new Dictionary<string, string>
      {
        ["key"] = "value"
      }
    };

    // Act
    string fileName = "secret.yaml";
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
  /// Verifies the generated Docker registry Secret object using V1Secret input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecret_ShouldGenerateAValidDockerRegistrySecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "docker-registry-secret",
        NamespaceProperty = "default"
      },
      Type = "kubernetes.io/dockerconfigjson",
      Data = new Dictionary<string, byte[]>
      {
        [".dockerconfigjson"] = Convert.FromBase64String("eyJhdXRocyI6eyJodHRwczovL2xvY2FsaG9zdDo4MDgwL2NvbnRhaW5lciI6eyJ1c2VybmFtZSI6InVzZXIiLCJwYXNzd29yZCI6InBhc3N3b3JkIn19fQ==")
      }
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
  /// Verifies the generated TLS Secret object using V1Secret input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithTLSSecret_ShouldGenerateAValidTLSSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret",
        NamespaceProperty = "default"
      },
      Type = "kubernetes.io/tls",
      Data = new Dictionary<string, byte[]>
      {
      }
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
