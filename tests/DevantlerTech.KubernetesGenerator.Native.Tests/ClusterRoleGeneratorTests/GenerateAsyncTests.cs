using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="ClusterRoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterRole object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole("cluster-role")
    {
      Verbs = ["get", "list", "watch"],
      Resources = ["pods", "services"],
      ResourceNames = ["readablepod", "anotherpod"],
      NonResourceUrls = ["/logs/*", "/metrics"]
    };

    // Act
    string fileName = "cluster-role.yaml";
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
  /// Verifies the generated ClusterRole object with aggregation rule.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAggregationRule_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole("monitoring")
    {
      AggregationRule = "rbac.example.com/aggregate-to-monitoring=true"
    };

    // Act
    string fileName = "aggregated-cluster-role.yaml";
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
  /// Verifies the generated ClusterRole object with simple verb and resource.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithSimpleVerbAndResource_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole("simple-role")
    {
      Verbs = ["get"],
      Resources = ["pods"]
    };

    // Act
    string fileName = "simple-cluster-role.yaml";
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
  /// Verifies the generated ClusterRole object with non-resource URLs only.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNonResourceUrls_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole("url-access")
    {
      Verbs = ["get"],
      NonResourceUrls = ["/logs/*", "/api/v1/version"]
    };

    // Act
    string fileName = "non-resource-cluster-role.yaml";
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
  /// Verifies the generated ClusterRole object with API Group specified.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithApiGroup_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole("app-access")
    {
      Verbs = ["get", "list", "watch"],
      Resources = ["rs.apps", "deployments.apps"]
    };

    // Act
    string fileName = "api-group-cluster-role.yaml";
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
  /// Verifies that a <see cref="ArgumentNullException"/> is thrown when the model is null.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateAsync(null!, Path.GetTempFileName()));
  }
}
