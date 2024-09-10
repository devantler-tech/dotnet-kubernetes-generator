using Devantler.KubernetesGenerator.Native.Authorization;
using k8s.Models;

namespace Devantler.KubernetesGenerator.Native.Tests.AuthorizationTests.ClusterRoleBindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterRoleBindingGenerator"/> class.
/// </summary>
public class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ClusterRoleBinding object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidClusterRoleBinding()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new V1ClusterRoleBinding
    {
      ApiVersion = "v1",
      Kind = "ClusterRoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role-binding",
        NamespaceProperty = "default"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "ClusterRole",
        Name = "cluster-role"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          NamespaceProperty = "default",
          Name = "user",
        }
      ]
    };

    // Act
    string outputPath = Path.Combine(Path.GetTempPath(), "cluster-role-binding.yaml");
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
