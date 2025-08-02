
using DevantlerTech.KubernetesGenerator.Native.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.Ingress;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.IngressGeneratorTests;

/// <summary>
/// Tests for the <see cref="IngressGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeIngress object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "simple-ingress", Namespace = "default" },
      Class = "nginx",
      Rules = [
        new NativeIngressRule
        {
          Host = "example.com",
          Path = "app",
          ServiceName = "app-service",
          ServicePort = "80"
        }
      ]
    };

    // Act
    string fileName = "ingress-basic.yaml";
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
  /// Verifies the generated NativeIngress object with TLS.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithTLS_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "tls-ingress", Namespace = "default" },
      Class = "nginx",
      Rules = [
        new NativeIngressRule
        {
          Host = "secure.example.com",
          Path = "app",
          ServiceName = "app-service",
          ServicePort = "443",
          TlsSecretName = "my-tls-secret"
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
  /// Verifies the generated NativeIngress object with default backend.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithDefaultBackend_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "default-backend-ingress", Namespace = "default" },
      Class = "nginx",
      DefaultBackend = "default-service:80",
      Rules = [
        new NativeIngressRule
        {
          Host = "example.com",
          Path = "app",
          ServiceName = "app-service",
          ServicePort = "80"
        }
      ]
    };

    // Act
    string fileName = "ingress-default-backend.yaml";
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
  /// Verifies the generated NativeIngress object with annotations.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAnnotations_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "annotated-ingress", Namespace = "default" },
      Class = "nginx",
      Rules = [
        new NativeIngressRule
        {
          Host = "example.com",
          Path = "app",
          ServiceName = "app-service",
          ServicePort = "80"
        }
      ],
      Annotations = new Dictionary<string, string>
      {
        { "nginx.ingress.kubernetes.io/rewrite-target", "/" },
        { "nginx.ingress.kubernetes.io/ssl-redirect", "true" }
      }
    };

    // Act
    string fileName = "ingress-annotations.yaml";
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
  /// Verifies the generated NativeIngress object with multiple rules.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleRules_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "multi-rule-ingress", Namespace = "default" },
      Class = "nginx",
      Rules = [
        new NativeIngressRule
        {
          Path = "",
          ServiceName = "api-service",
          ServicePort = "80"
        },
        new NativeIngressRule
        {
          Host = "web.example.com",
          ServiceName = "web-service",
          ServicePort = "80"
        },
        new NativeIngressRule
        {
          Host = "admin.example.com",
          Path = "admin*",
          ServiceName = "admin-service",
          ServicePort = "8080"
        }
      ]
    };

    // Act
    string fileName = "ingress-multiple-rules.yaml";
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
  /// Verifies the generated NativeIngress object with name only (no namespace).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNameOnly_ShouldGenerateAValidIngress()
  {
    // Arrange
    var generator = new IngressGenerator();
    var model = new NativeIngress
    {
      Metadata = new Metadata { Name = "simple-ingress" },
      Rules = [
        new NativeIngressRule
        {
          Host = "example.com",
          Path = "",
          ServiceName = "app-service",
          ServicePort = "80"
        }
      ]
    };

    // Act
    string fileName = "ingress-name-only.yaml";
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

