using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class with kubectl create functionality.
/// </summary>
public class KubectlCreateTests
{
  /// <summary>
  /// Verifies that GenerateAsync creates a valid kubectl-generated RoleBinding.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithKubectlCreate_ShouldGenerateValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "test-role-binding",
        NamespaceProperty = "default"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "Role",
        Name = "test-role"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          Name = "test-user",
        }
      ]
    };

    // Act
    string fileName = "test-role-binding-kubectl.yaml";
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);
    
    await generator.GenerateAsync(model, outputPath);
    
    // Assert
    Assert.True(File.Exists(outputPath), "Output file should be created");
    
    string fileContent = await File.ReadAllTextAsync(outputPath);
    Assert.NotEmpty(fileContent);
    
    // Verify the generated YAML contains expected elements
    Assert.Contains("apiVersion: rbac.authorization.k8s.io/v1", fileContent);
    Assert.Contains("kind: RoleBinding", fileContent);
    Assert.Contains("name: test-role-binding", fileContent);
    Assert.Contains("namespace: default", fileContent);
    Assert.Contains("roleRef:", fileContent);
    Assert.Contains("subjects:", fileContent);
    
    // Cleanup
    File.Delete(outputPath);
  }
}