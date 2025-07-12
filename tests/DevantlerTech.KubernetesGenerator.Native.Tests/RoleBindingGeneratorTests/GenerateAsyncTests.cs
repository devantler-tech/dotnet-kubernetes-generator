using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated RoleBinding object with all properties set.
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

  /// <summary>
  /// Verifies the generated RoleBinding object with ClusterRole reference.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithClusterRole_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "cluster-role-binding",
        NamespaceProperty = "default"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "ClusterRole",
        Name = "admin"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          Name = "admin-user",
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
  /// Verifies the generated RoleBinding object with multiple subjects.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateAValidRoleBinding()
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
        Kind = "Role",
        Name = "reader"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          Name = "user1",
        },
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "Group",
          Name = "readers",
        },
        new Rbacv1Subject
        {
          Kind = "ServiceAccount",
          Name = "reader-sa",
          NamespaceProperty = "default"
        }
      ]
    };

    // Act
    string fileName = "multi-subject-binding.yaml";
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
  /// Verifies the generated RoleBinding object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "simple-binding"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "Role",
        Name = "simple-role"
      },
      Subjects =
      [
        new Rbacv1Subject
        {
          ApiGroup = "rbac.authorization.k8s.io",
          Kind = "User",
          Name = "simple-user",
        }
      ]
    };

    // Act
    string fileName = "simple-binding.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a RoleRef set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutRoleRef_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "invalid-binding"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has an invalid RoleRef kind.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidRoleRefKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "invalid-binding"
      },
      RoleRef = new V1RoleRef
      {
        ApiGroup = "rbac.authorization.k8s.io",
        Kind = "InvalidRole",
        Name = "invalid"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has an invalid subject kind.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidSubjectKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new V1RoleBinding
    {
      ApiVersion = "rbac.authorization.k8s.io/v1",
      Kind = "RoleBinding",
      Metadata = new V1ObjectMeta
      {
        Name = "invalid-binding"
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
          Kind = "InvalidSubject",
          Name = "invalid",
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
