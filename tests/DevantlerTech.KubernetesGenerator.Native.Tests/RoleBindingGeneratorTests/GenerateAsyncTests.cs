using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
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
    var model = new RoleBinding("role-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = RoleBindingRoleRefKind.Role,
        Name = "role"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.User,
          Name = "user",
        }
      ]
    };
    model.Metadata.Namespace = "default";

    // Act
    string fileName = "role-binding.yaml";
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

  /// <summary>
  /// Verifies the generated RoleBinding object with ClusterRole reference.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithClusterRole_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new RoleBinding("cluster-role-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = RoleBindingRoleRefKind.ClusterRole,
        Name = "admin"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.User,
          Name = "admin-user",
        }
      ]
    };
    model.Metadata.Namespace = "default";

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

  /// <summary>
  /// Verifies the generated RoleBinding object with multiple subjects.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new RoleBinding("multi-subject-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = RoleBindingRoleRefKind.Role,
        Name = "reader"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.User,
          Name = "user1",
        },
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.Group,
          Name = "readers",
        },
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.ServiceAccount,
          Name = "reader-sa",
          Namespace = "default"
        }
      ]
    };
    model.Metadata.Namespace = "default";

    // Act
    string fileName = "multi-subject-binding.yaml";
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

  /// <summary>
  /// Verifies the generated RoleBinding object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new RoleBinding("simple-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = RoleBindingRoleRefKind.Role,
        Name = "simple-role"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.User,
          Name = "simple-user",
        }
      ]
    };

    // Act
    string fileName = "simple-binding.yaml";
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

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has an invalid RoleRef kind.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidRoleRefKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new RoleBinding("invalid-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = (RoleBindingRoleRefKind)999, // Invalid enum value
        Name = "invalid"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = RoleBindingSubjectKind.User,
          Name = "test-user"
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName())).ConfigureAwait(false);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has an invalid subject kind.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidSubjectKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new RoleBinding("invalid-binding")
    {
      RoleRef = new RoleBindingRoleRef
      {
        Kind = RoleBindingRoleRefKind.Role,
        Name = "role"
      },
      Subjects =
      [
        new RoleBindingSubject
        {
          Kind = (RoleBindingSubjectKind)999, // Invalid enum value
          Name = "invalid",
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName())).ConfigureAwait(false);
  }
}
