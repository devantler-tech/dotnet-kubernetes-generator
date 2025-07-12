using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated RoleBinding object.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "role-binding",
        NamespaceProperty = "default"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "Role",
        Name = "role"
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
    string fileName = "role-binding.yaml";
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
