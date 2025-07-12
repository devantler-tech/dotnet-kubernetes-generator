using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.SecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="SecretGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Secret object using generic secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new SecretCreateOptions
    {
      Generic = new GenericSecretOptions
      {
        Name = "secret",
        Namespace = "default",
        Type = "Opaque"
      }
    };

    // Add a literal value to match the expected output
    model.Generic.FromLiterals["key"] = "value";

    // Act
    string fileName = "secret.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated Docker registry secret.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDockerRegistrySecret_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new SecretCreateOptions
    {
      DockerRegistry = new DockerRegistrySecretOptions
      {
        Name = "docker-secret",
        Namespace = "default",
        DockerServer = "https://registry.example.com",
        DockerUsername = "testuser",
        DockerPassword = "testpass",
        DockerEmail = "test@example.com"
      }
    };

    // Act
    string fileName = "docker-secret.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

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

    // Create temporary cert and key files
    string certPath = Path.Combine(Path.GetTempPath(), "test.crt");
    string keyPath = Path.Combine(Path.GetTempPath(), "test.key");

    await File.WriteAllTextAsync(certPath, "-----BEGIN CERTIFICATE-----\nMIIBkTCB+wIJAN0KX...\n-----END CERTIFICATE-----").ConfigureAwait(false);
    await File.WriteAllTextAsync(keyPath, "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC...\n-----END PRIVATE KEY-----").ConfigureAwait(false);

    var model = new SecretCreateOptions
    {
      Tls = new TlsSecretOptions
      {
        Name = "tls-secret",
        Namespace = "default",
        CertPath = certPath,
        KeyPath = keyPath
      }
    };

    try
    {
      // Act
      string fileName = "tls-secret.yaml";
      string outputPath = Path.Combine(Path.GetTempPath(), fileName);
      if (File.Exists(outputPath))
        File.Delete(outputPath);
      await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
      string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

      // Assert
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

      // Cleanup
      File.Delete(outputPath);
    }
    finally
    {
      // Cleanup temp files
      if (File.Exists(certPath))
        File.Delete(certPath);
      if (File.Exists(keyPath))
        File.Delete(keyPath);
    }
  }

  /// <summary>
  /// Verifies the generated generic secret with multiple literals.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretMultipleLiterals_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new SecretCreateOptions
    {
      Generic = new GenericSecretOptions
      {
        Name = "multi-secret",
        Namespace = "test-namespace",
        Type = "Opaque"
      }
    };

    // Add multiple literal values
    model.Generic.FromLiterals["username"] = "admin";
    model.Generic.FromLiterals["password"] = "secret123";
    model.Generic.FromLiterals["api-key"] = "abcd1234";

    // Act
    string fileName = "multi-secret.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated generic secret with from-file option.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretFromFile_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();

    // Create a temporary file
    string tempFilePath = Path.Combine(Path.GetTempPath(), "config.txt");
    await File.WriteAllTextAsync(tempFilePath, "configuration data").ConfigureAwait(false);

    var model = new SecretCreateOptions
    {
      Generic = new GenericSecretOptions
      {
        Name = "file-secret",
        Namespace = "default"
      }
    };

    // Add file source
    model.Generic.FromFiles.Add($"config={tempFilePath}");

    try
    {
      // Act
      string fileName = "file-secret.yaml";
      string outputPath = Path.Combine(Path.GetTempPath(), fileName);
      if (File.Exists(outputPath))
        File.Delete(outputPath);
      await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
      string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

      // Assert
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

      // Cleanup
      File.Delete(outputPath);
    }
    finally
    {
      // Cleanup temp file
      if (File.Exists(tempFilePath))
        File.Delete(tempFilePath);
    }
  }

  /// <summary>
  /// Verifies the generated generic secret with append hash option.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretAppendHash_ShouldGenerateAValidSecret()
  {
    // Arrange
    var generator = new SecretGenerator();
    var model = new SecretCreateOptions
    {
      Generic = new GenericSecretOptions
      {
        Name = "hash-secret",
        Namespace = "default",
        Type = "Opaque",
        AppendHash = true
      }
    };

    model.Generic.FromLiterals["data"] = "test-data";

    // Act
    string fileName = "hash-secret.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}
