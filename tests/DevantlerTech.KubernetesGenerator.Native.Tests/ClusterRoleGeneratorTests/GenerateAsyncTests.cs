using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterRoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterRole object with aggregation rule.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAggregationRule_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "cluster-role-aggregation",
      AggregationRule = new AggregationRule
      {
        ClusterRoleSelectors =
        [
          new LabelSelector
          {
            MatchLabels = { { "rbac.example.com/aggregate-to-monitoring", "true" } }
          }
        ]
      }
    };

    // Act
    string fileName = "cluster-role-aggregation.yaml";
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
  /// Verifies the generated ClusterRole object with non-resource URLs.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNonResourceURLs_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "cluster-role-non-resource-urls",
      Rules =
      [
        new PolicyRule
        {
          NonResourceURLs = ["/metrics"],
          Verbs = ["get"]
        }
      ]
    };

    // Act
    string fileName = "cluster-role-non-resource-urls.yaml";
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
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "", // Empty name should cause validation error
      Rules =
      [
        new PolicyRule
        {
          ApiGroups = [""],
          Resources = ["pods"],
          Verbs = ["get"]
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has multiple rules.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithMultipleRules_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "cluster-role-multiple-rules",
      Rules =
      [
        new PolicyRule
        {
          ApiGroups = [""],
          Resources = ["pods"],
          Verbs = ["get"]
        },
        new PolicyRule
        {
          ApiGroups = ["apps"],
          Resources = ["deployments"],
          Verbs = ["get"]
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that ArgumentNullException is thrown when the model is null.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithNullModel_ShouldThrowArgumentNullException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();

    // Act & Assert
    _ = await Assert.ThrowsAsync<ArgumentNullException>(() => generator.GenerateAsync(null!, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has empty rules.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithEmptyRules_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "cluster-role-empty-rules",
      Rules = []
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has a rule with no verbs.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithRuleWithoutVerbs_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Name = "cluster-role-no-verbs",
      Rules =
      [
        new PolicyRule
        {
          ApiGroups = [""],
          Resources = ["pods"],
          Verbs = [] // Empty verbs
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
