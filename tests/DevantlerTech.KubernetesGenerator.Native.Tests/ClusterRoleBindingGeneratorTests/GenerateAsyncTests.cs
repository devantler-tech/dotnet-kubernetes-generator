using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleBindingGeneratorTests;


/// <summary>
/// Tests for the <see cref="ClusterRoleBindingGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
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
    await generator.GenerateAsync(model, outputPath);
    string fileContent = await File.ReadAllTextAsync(outputPath);

    // Assert
    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    // Cleanup
    File.Delete(outputPath);
  }

  /// <summary>
  /// Verifies the generated ClusterRoleBinding object with multiple subjects.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateAValidClusterRoleBinding()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new V1ClusterRoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "multi-subject-binding"
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
          Kind = "User",
          Name = "bob",
        },
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "Group",
          Name = "admins",
        },
        new Rbacv1Subject
        {
          ApiGroup = "",
          Kind = "ServiceAccount",
          Name = "system-sa",
          NamespaceProperty = "kube-system"
        }
      ]
    };

    // Act
    string fileName = "multi-subject-cluster-role-binding.yaml";
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
  /// Verifies that an exception is thrown when no name is provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowException()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new V1ClusterRoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = null // No name provided
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "ClusterRole",
        Name = "cluster-role"
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "test.yaml");
    var exception = await Assert.ThrowsAsync<DevantlerTech.KubernetesGenerator.Core.KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, outputPath));

    Assert.Contains("model.Metadata.Name must be set", exception.Message, StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Verifies that an exception is thrown when no cluster role is provided.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutClusterRole_ShouldThrowException()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new V1ClusterRoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "ClusterRoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "test-binding"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "ClusterRole",
        Name = null // No cluster role provided
      }
    };

    // Act & Assert
    string outputPath = Path.Combine(Path.GetTempPath(), "test.yaml");
    var exception = await Assert.ThrowsAsync<DevantlerTech.KubernetesGenerator.Core.KubernetesGeneratorException>(
      () => generator.GenerateAsync(model, outputPath));

    Assert.Contains("model.RoleRef.Name must be set", exception.Message, StringComparison.OrdinalIgnoreCase);
  }
}
