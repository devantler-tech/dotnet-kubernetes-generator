using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ServiceGeneratorTests;


/// <summary>
/// Tests for the <see cref="ServiceGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Service object with ClusterIP type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithClusterIPService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        ClusterIP = "192.168.34.21",
        Ports =
        [
          new V1ServicePort
          {
            Name = "port-name",
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080
          }
        ],
        Selector = new Dictionary<string, string>
        {
          ["app"] = "app"
        },
        Type = "ClusterIP"
      }
    };

    // Act
    string fileName = "service.yaml";
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
  /// Verifies the generated Service object with NodePort type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNodePortService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "nodeport-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        Ports =
        [
          new V1ServicePort
          {
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080,
            NodePort = 30080
          }
        ],
        Type = "NodePort"
      }
    };

    // Act
    string fileName = "nodeport-service.yaml";
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
  /// Verifies the generated Service object with LoadBalancer type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLoadBalancerService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "loadbalancer-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        Ports =
        [
          new V1ServicePort
          {
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080
          }
        ],
        Type = "LoadBalancer"
      }
    };

    // Act
    string fileName = "loadbalancer-service.yaml";
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
  /// Verifies the generated Service object with ExternalName type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithExternalNameService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "external-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        Type = "ExternalName",
        ExternalName = "external.example.com"
      }
    };

    // Act
    string fileName = "external-service.yaml";
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
  /// Verifies the generated Service object with headless ClusterIP (None).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithHeadlessService_ShouldGenerateAValidService()
  {
    // Arrange
    var generator = new ServiceGenerator();
    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "headless-service",
        NamespaceProperty = "default"
      },
      Spec = new V1ServiceSpec
      {
        ClusterIP = "None",
        Ports =
        [
          new V1ServicePort
          {
            Port = 80,
            Protocol = "TCP",
            TargetPort = 8080
          }
        ],
        Type = "ClusterIP"
      }
    };

    // Act
    string fileName = "headless-service.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithServiceWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the service type is not supported.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithUnsupportedServiceType_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "invalid-service"
      },
      Spec = new V1ServiceSpec
      {
        Type = "InvalidType"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when ExternalName service doesn't have ExternalName set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithExternalNameServiceWithoutExternalName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ServiceGenerator();

    var model = new V1Service
    {
      ApiVersion = "v1",
      Kind = "Service",
      Metadata = new V1ObjectMeta
      {
        Name = "external-service"
      },
      Spec = new V1ServiceSpec
      {
        Type = "ExternalName"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}

