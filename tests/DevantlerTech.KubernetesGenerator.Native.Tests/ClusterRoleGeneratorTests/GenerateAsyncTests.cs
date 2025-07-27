using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="ClusterRoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterRole object with basic rules.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicRule_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Metadata = new Metadata
      {
        Name = "pod-reader"
      },
      Rules =
      [
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get, ClusterRoleVerb.List, ClusterRoleVerb.Watch],
          ApiGroups = [""],
          Resources = ["pods"]
        }
      ]
    };

    // Act
    string fileName = "cluster-role-basic.yaml";
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
  /// Verifies the generated ClusterRole object with resource names.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithResourceNames_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Metadata = new Metadata
      {
        Name = "specific-pod-reader"
      },
      Rules =
      [
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get],
          ApiGroups = [""],
          Resources = ["pods"],
          ResourceNames = ["my-pod", "another-pod"]
        }
      ]
    };

    // Act
    string fileName = "cluster-role-resource-names.yaml";
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
  /// Verifies the generated ClusterRole object with API groups.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithApiGroups_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Metadata = new Metadata
      {
        Name = "replica-set-manager"
      },
      Rules =
      [
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get, ClusterRoleVerb.List, ClusterRoleVerb.Watch],
          ApiGroups = ["apps"],
          Resources = ["replicasets"]
        }
      ]
    };

    // Act
    string fileName = "cluster-role-api-groups.yaml";
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
  public async Task GenerateAsync_WithNonResourceUrls_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Metadata = new Metadata
      {
        Name = "log-reader"
      },
      Rules =
      [
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get],
          NonResourceURLs = ["/logs/*", "/metrics"]
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
      Metadata = new Metadata
      {
        Name = "monitoring"
      },
      AggregationRule = new ClusterRoleAggregationRule
      {
        ClusterRoleSelectors =
        [
          new ClusterRoleLabelSelector
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
  /// Verifies the generated ClusterRole object with comprehensive features.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveFeatures_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new ClusterRole
    {
      Metadata = new Metadata
      {
        Name = "comprehensive-role",
        Labels = new Dictionary<string, string>
        {
          { "app", "test" },
          { "version", "v1" }
        },
        Annotations = new Dictionary<string, string>
        {
          { "description", "A comprehensive cluster role for testing" }
        }
      },
      Rules =
      [
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get, ClusterRoleVerb.List, ClusterRoleVerb.Watch],
          ApiGroups = [""],
          Resources = ["pods", "services"]
        },
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get, ClusterRoleVerb.List],
          ApiGroups = ["apps"],
          Resources = ["deployments", "replicasets"],
          ResourceNames = ["my-deployment"]
        },
        new ClusterRoleRule
        {
          Verbs = [ClusterRoleVerb.Get],
          NonResourceURLs = ["/api/*", "/metrics"]
        }
      ]
    };

    // Act
    string fileName = "cluster-role-comprehensive.yaml";
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
