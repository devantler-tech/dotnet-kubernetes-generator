using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.IngressGeneratorTests;

/// <summary>
/// Tests for the <see cref="IngressGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Ingress object with all properties set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new Ingress
    {
      Metadata = new V1ObjectMeta
      {
        Name = "ingress",
        NamespaceProperty = "default"
      },
      IngressClassName = "nginx",
      DefaultBackend = new IngressBackend
      {
        ServiceName = "service-name",
        ServicePort = "80"
      },
      Rules =
      [
        new IngressRule
        {
          Host = "host",
          Path = "/path",
          Backend = new IngressBackend
          {
            ServiceName = "service-name",
            ServicePort = "80"
          }
        }
      ]
    };

    // Act
    string fileName = "ingress.yaml";
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
  /// Verifies the generated Ingress object with minimal required fields.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithMinimalProperties_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new Ingress
    {
      Metadata = new V1ObjectMeta
      {
        Name = "ingress-minimal"
      },
      DefaultBackend = new IngressBackend
      {
        ServiceName = "default-service",
        ServicePort = "80"
      }
    };

    // Act
    string fileName = "ingress-minimal.yaml";
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
  /// Verifies the generated Ingress object with TLS configuration.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithTlsConfiguration_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new Ingress
    {
      Metadata = new V1ObjectMeta
      {
        Name = "ingress-tls",
        NamespaceProperty = "default"
      },
      IngressClassName = "nginx",
      Rules =
      [
        new IngressRule
        {
          Host = "example.com",
          Path = "/",
          Backend = new IngressBackend
          {
            ServiceName = "web-service",
            ServicePort = "80"
          },
          TlsSecretName = "tls-secret"
        }
      ]
    };

    // Act
    string fileName = "ingress-tls.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the Ingress model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithIngressWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new Ingress
    {
      Metadata = new V1ObjectMeta
      {
        NamespaceProperty = "default"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

