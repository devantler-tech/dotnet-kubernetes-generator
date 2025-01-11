using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.ClusterRoleGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterRoleGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterRole object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidClusterRole()
  {
    // Arrange
    var generator = new ClusterRoleGenerator();
    var model = new V1ClusterRole
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRole",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role",
        NamespaceProperty = "default"
      },
      AggregationRule = new V1AggregationRule
      {
        ClusterRoleSelectors =
        [
          new V1LabelSelector
          {
            MatchLabels = new Dictionary<string, string>
            {
              { "key", "value" }
            },
            MatchExpressions =
            [
              new V1LabelSelectorRequirement
              {
                Key = "key",
                OperatorProperty = "operator",
                Values = ["value"]
              }
            ]
          }
        ]
      },
      Rules =
      [
        new V1PolicyRule
        {
          ApiGroups = ["api-group"],
          NonResourceURLs = ["url"],
          ResourceNames = ["resource-name"],
          Resources = ["resource"],
          Verbs = ["verb"],
        }
      ]
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
}
