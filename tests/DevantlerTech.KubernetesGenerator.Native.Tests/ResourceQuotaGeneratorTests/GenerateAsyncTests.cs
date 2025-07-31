using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ResourceQuotaGeneratorTests;


/// <summary>
/// Tests for the <see cref="ResourceQuotaGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ResourceQuota object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidResourceQuota()
  {
    // Arrange
    var generator = new ResourceQuotaGenerator();
    var model = new ResourceQuota("resource-quota")
    {
      Metadata = { Namespace = "default" },
      Hard = new Dictionary<string, ResourceQuantity>
      {
        ["requests.cpu"] = new ResourceQuantity("1"),
        ["requests.memory"] = new ResourceQuantity("1Gi"),
        ["limits.cpu"] = new ResourceQuantity("1"),
        ["limits.memory"] = new ResourceQuantity("1Gi")
      }
    };

    // Act
    string fileName = "resource-quota.yaml";
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
  /// Verifies the generated ResourceQuota object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidResourceQuota()
  {
    // Arrange
    var generator = new ResourceQuotaGenerator();
    var model = new ResourceQuota("resource-quota-no-namespace")
    {
      Hard = new Dictionary<string, ResourceQuantity>
      {
        ["requests.cpu"] = new ResourceQuantity("2"),
        ["requests.memory"] = new ResourceQuantity("2Gi")
      }
    };

    // Act
    string fileName = "resource-quota-no-namespace.yaml";
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
  /// Verifies the generated ResourceQuota object with scopes.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithScopes_ShouldGenerateAValidResourceQuota()
  {
    // Arrange
    var generator = new ResourceQuotaGenerator();
    var model = new ResourceQuota("resource-quota-with-scopes")
    {
      Metadata = { Namespace = "test-namespace" },
      Hard = new Dictionary<string, ResourceQuantity>
      {
        ["pods"] = new ResourceQuantity("10")
      },
      Scopes = ["BestEffort", "NotBestEffort"]
    };

    // Act
    string fileName = "resource-quota-with-scopes.yaml";
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
  /// Verifies the generated ResourceQuota object without hard limits.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutHardLimits_ShouldGenerateAValidResourceQuota()
  {
    // Arrange
    var generator = new ResourceQuotaGenerator();
    var model = new ResourceQuota("resource-quota-no-limits")
    {
      Metadata = { Namespace = "default" },
      Scopes = ["BestEffort"]
    };

    // Act
    string fileName = "resource-quota-no-limits.yaml";
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
