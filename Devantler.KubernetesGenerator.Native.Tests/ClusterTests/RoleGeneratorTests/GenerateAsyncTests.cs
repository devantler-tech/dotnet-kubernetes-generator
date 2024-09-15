using Devantler.KubernetesGenerator.Native.Cluster;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.ClusterTests.RoleGeneratorTests;


/// <summary>
/// Tests for the <see cref="RoleGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Role object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new V1Role
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "Role",
      Metadata = new V1ObjectMeta
      {
        Name = "role",
        NamespaceProperty = "default"
      },
      Rules =
      [
        new V1PolicyRule
        {
          ApiGroups = ["api-group"],
          NonResourceURLs = ["url"],
          ResourceNames = ["resource-name"],
          Resources = ["resource"],
          Verbs = ["verb"]
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "role.yaml");
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent);

    // Cleanup
    File.Delete(outputPath);
  }
}
