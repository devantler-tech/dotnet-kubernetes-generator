using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.TLSSecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="TLSSecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated TLS Secret object with certificate and key content.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithTLSSecretFromContent_ShouldGenerateAValidTLSSecret()
  {
    // Arrange
    var generator = new TLSSecretGenerator();
    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret-content",
        NamespaceProperty = "default"
      },
      Certificate = "-----BEGIN CERTIFICATE-----\nMIIBkTCB+wIJAK7z7zxzWh5HMA0GCSqGSIb3DQEBCwUAMBQxEjAQBgNVBAoMCWxv\nY2FsaG9zdDAeFw0yMzEwMDEwMDAwMDBaFw0yNDEwMDEwMDAwMDBaMBQxEjAQBgNV\nBAoMCWxvY2FsaG9zdDBcMA0GCSqGSIb3DQEBAQUAA0sAMEgCQQDYYWZhkYb2dM9x\nKVNLQj6qJhfnW6fTvQXYJOhL+s0WTEYqEBF9v8nXjJzDvEtPNqW4e8mJbMCVMtCY\nZCkWkqNyAgMBAAEwDQYJKoZIhvcNAQELBQADQQBwXyRqGvyQmYjF5sJGPGjkgGlT\nYUg6qjNWDnBYXOdZhYj2F2YLAJkzSCzSNbGQUxbGV0cA==\n-----END CERTIFICATE-----",
      PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIBVQIBADANBgkqhkiG9w0BAQEFAASCAT8wggE7AgEAAkEA2GFmYZGG9nTPcSlT\nS0I+qiYX51un070F2CToS/rNFkxGKhARfb/J14ycw7xLTzaluHvJiWzAlTLQmGQp\nFpKjcgIDAQABAkEAyFvP3PBgxJCrN6PqfNpvU+J8rAhXJnqLKRd7zg3VZhqEhzO\nWqjNRZjpJYTMYXZjYFfMHcPQ1TqMXrCnKQcQCwJAIhANBgkqhkiG9w0BAQEFAASB\nJUAIhANBgkqhkiG9w0BAQEFAASBJUA==\n-----END PRIVATE KEY-----"
    };

    // Act
    string fileName = "tls-secret-content.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);

    // Clean up temp files created during generation
    string[] tempFiles = Directory.GetFiles(Path.GetTempPath(), "tls-*");
    foreach (string tempFile in tempFiles)
    {
      File.Delete(tempFile);
    }
  }

  /// <summary>
  /// Verifies the generated TLS Secret object with certificate and key file paths.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithTLSSecretFromFiles_ShouldGenerateAValidTLSSecret()
  {
    // Arrange
    var generator = new TLSSecretGenerator();

    // Create temporary certificate and key files
    string certPath = Path.Combine(Path.GetTempPath(), "test-cert.crt");
    string keyPath = Path.Combine(Path.GetTempPath(), "test-key.key");

    await File.WriteAllTextAsync(certPath, "-----BEGIN CERTIFICATE-----\nMIIBkTCB+wIJAK7z7zxzWh5HMA0GCSqGSIb3DQEBCwUAMBQxEjAQBgNVBAoMCWxv\nY2FsaG9zdDAeFw0yMzEwMDEwMDAwMDBaFw0yNDEwMDEwMDAwMDBaMBQxEjAQBgNV\nBAoMCWxvY2FsaG9zdDBcMA0GCSqGSIb3DQEBAQUAA0sAMEgCQQDYYWZhkYb2dM9x\nKVNLQj6qJhfnW6fTvQXYJOhL+s0WTEYqEBF9v8nXjJzDvEtPNqW4e8mJbMCVMtCY\nZCkWkqNyAgMBAAEwDQYJKoZIhvcNAQELBQADQQBwXyRqGvyQmYjF5sJGPGjkgGlT\nYUg6qjNWDnBYXOdZhYj2F2YLAJkzSCzSNbGQUxbGV0cA==\n-----END CERTIFICATE-----");
    await File.WriteAllTextAsync(keyPath, "-----BEGIN PRIVATE KEY-----\nMIIBVQIBADANBgkqhkiG9w0BAQEFAASCAT8wggE7AgEAAkEA2GFmYZGG9nTPcSlT\nS0I+qiYX51un070F2CToS/rNFkxGKhARfb/J14ycw7xLTzaluHvJiWzAlTLQmGQp\nFpKjcgIDAQABAkEAyFvP3PBgxJCrN6PqfNpvU+J8rAhXJnqLKRd7zg3VZhqEhzO\nWqjNRZjpJYTMYXZjYFfMHcPQ1TqMXrCnKQcQCwJAIhANBgkqhkiG9w0BAQEFAASB\nJUAIhANBgkqhkiG9w0BAQEFAASBJUA==\n-----END PRIVATE KEY-----");

    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret-files",
        NamespaceProperty = "default"
      },
      Certificate = certPath,
      PrivateKey = keyPath
    };

    // Act
    string fileName = "tls-secret-files.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
    File.Delete(certPath);
    File.Delete(keyPath);
  }
}
