using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

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
    var model = new V1ClusterRole
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRole",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role-aggregation"
      },
      AggregationRule = new V1AggregationRule
      {
        ClusterRoleSelectors =
        [
          new V1LabelSelector
          {
            MatchLabels = new Dictionary<string, string>
            {
              { "rbac.example.com/aggregate-to-monitoring", "true" }
            }
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
    var model = new V1ClusterRole
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRole",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role-non-resource-urls"
      },
      Rules =
      [
        new V1PolicyRule
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
    var model = new V1ClusterRole
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRole"
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
    var model = new V1ClusterRole
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRole",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role-multiple-rules"
      },
      Rules =
      [
        new V1PolicyRule
        {
          ApiGroups = [""],
          Resources = ["pods"],
          Verbs = ["get"]
        },
        new V1PolicyRule
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
}
