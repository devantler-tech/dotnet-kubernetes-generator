using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "role-binding",
        Namespace = "default"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "Role",
        Name = "role"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = "User",
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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "cluster-role-binding",
        Namespace = "default"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "ClusterRole",
        Name = "admin"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "multi-subject-binding",
        Namespace = "default"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "Role",
        Name = "reader"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = "User",
          Name = "user1",
        },
        new RoleBindingSubject
        {
          Kind = "Group",
          Name = "readers",
        },
        new RoleBindingSubject
        {
          Kind = "ServiceAccount",
          Name = "reader-sa",
          Namespace = "default"
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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "simple-binding"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "Role",
        Name = "simple-role"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
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
    var model = new RoleBinding
    {
      Metadata = null,
      RoleRef = new RoleBindingRoleRef { Kind = "Role", Name = "test-role" },
      Subjects = []
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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "invalid-binding"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "InvalidRole",
        Name = "invalid"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = "User",
          Name = "test-user"
        }
      ]
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
    var model = new RoleBinding
    {
      Metadata = new Metadata
      {
        Name = "invalid-binding"
      },
      RoleRef = new RoleBindingRoleRef
      {
        Kind = "Role",
        Name = "role"
      },
      Subjects =
      [
        new RoleBindingSubject
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
