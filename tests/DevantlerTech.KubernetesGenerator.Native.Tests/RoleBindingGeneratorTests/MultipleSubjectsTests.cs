using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class with multiple subjects.
/// </summary>
public class MultipleSubjectsTests
{
  /// <summary>
  /// Verifies that GenerateAsync handles multiple subjects correctly.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "multi-subject-binding",
        NamespaceProperty = "default"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "ClusterRole",
        Name = "cluster-admin"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          Name = "alice",
        },
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "Group",
          Name = "developers",
        },
        new Rbacv1Subject
        {
          Kind = "ServiceAccount",
          Name = "default",
          NamespaceProperty = "kube-system"
        }
      ]
    };

    // Act
    string fileName = "multi-subject-binding.yaml";
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
    Assert.Contains("name: multi-subject-binding", fileContent);
    Assert.Contains("namespace: default", fileContent);
    Assert.Contains("roleRef:", fileContent);
    Assert.Contains("subjects:", fileContent);
    
    // Verify all subjects are present
    Assert.Contains("alice", fileContent);
    Assert.Contains("developers", fileContent);
    Assert.Contains("default", fileContent);
    Assert.Contains("kube-system", fileContent);
    
    // Cleanup
    File.Delete(outputPath);
  }
}