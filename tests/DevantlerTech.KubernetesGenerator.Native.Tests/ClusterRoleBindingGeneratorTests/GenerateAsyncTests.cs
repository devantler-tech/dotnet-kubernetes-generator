using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleBindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterRoleBindingGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
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
      ApiVersion = "rbac.authorization.k8s.io/v1",
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
    string fileName = "cluster-role-binding.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }
}
