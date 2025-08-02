
using DevantlerTech.KubernetesGenerator.Native.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.ClusterRole;
using DevantlerTech.KubernetesGenerator.Native.Models.Role;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="ClusterRoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeClusterRole object with basic rules.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicRule_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "pod-reader"
      },
      Rules =
      [
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get, NativeRoleVerb.List, NativeRoleVerb.Watch],
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
  /// Verifies the generated NativeClusterRole object with resource names.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithResourceNames_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "specific-pod-reader"
      },
      Rules =
      [
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get],
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
  /// Verifies the generated NativeClusterRole object with API groups.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithApiGroups_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "replica-set-manager"
      },
      Rules =
      [
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get, NativeRoleVerb.List, NativeRoleVerb.Watch],
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
  /// Verifies the generated NativeClusterRole object with non-resource URLs.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNonResourceUrls_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "log-reader"
      },
      Rules =
      [
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get],
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
  /// Verifies the generated NativeClusterRole object with aggregation rule.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAggregationRule_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
      {
        Name = "monitoring"
      },
      AggregationRule = new NativeClusterRoleAggregationRule
      {
        ClusterRoleSelectors =
        [
          new NativeClusterRoleLabelSelector
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
  /// Verifies the generated NativeClusterRole object with comprehensive features.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveFeatures_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new NativeClusterRole
    {
      Metadata = new ClusterScopedMetadata
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
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get, NativeRoleVerb.List, NativeRoleVerb.Watch],
          ApiGroups = [""],
          Resources = ["pods", "services"]
        },
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get, NativeRoleVerb.List],
          ApiGroups = ["apps"],
          Resources = ["deployments", "replicasets"],
          ResourceNames = ["my-deployment"]
        },
        new NativeClusterRoleRule
        {
          Verbs = [NativeRoleVerb.Get],
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
