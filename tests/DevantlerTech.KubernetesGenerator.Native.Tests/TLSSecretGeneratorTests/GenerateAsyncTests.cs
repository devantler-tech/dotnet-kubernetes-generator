using DevantlerTech.KubernetesGenerator.Core;
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
    // Read the contents of the certificate and key files
    string certificateContent = await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.crt"));
    string privateKeyContent = await File.ReadAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.key"));

    var generator = new TLSSecretGenerator();
    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret-content",
        NamespaceProperty = "default"
      },
      Certificate = certificateContent,
      PrivateKey = privateKeyContent
    };

    // Act
    string fileName = "tls-secret-content.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    if (!OperatingSystem.IsWindows())
    {
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);
    }

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

    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret-files",
        NamespaceProperty = "default"
      },
      Certificate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.crt"),
      PrivateKey = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.key")
    };

    // Act
    string fileName = "tls-secret-files.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    if (!OperatingSystem.IsWindows())
    {
      _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);
    }

    // Cleanup
    File.Delete(outputPath);
  }

  // Add a test that tests that the throw new KubernetesGeneratorException("The model.Metadata.Name must be set to set the secret name."); is thrown when the name is not set
  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the TLSSecret model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithTLSSecretWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new TLSSecretGenerator();

    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      },
      Certificate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.crt"),
      PrivateKey = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "tls.key")
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }


  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when invalid certificate or private key data is provided.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidCertificateData_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new TLSSecretGenerator();

    var model = new TLSSecret
    {
      Metadata = new V1ObjectMeta
      {
        Name = "tls-secret-invalid-cert",
        NamespaceProperty = "default"
      },
      Certificate = "invalid-certificate-data",
      PrivateKey = "invalid-private-key-data"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
