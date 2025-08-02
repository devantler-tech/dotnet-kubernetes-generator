using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;
using DevantlerTech.KubernetesGenerator.Native.Models.RoleBinding;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleBindingGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeRoleBinding object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "role-binding", Namespace = "default" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.Role,
        Name = "role"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.User,
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
  /// Verifies the generated NativeRoleBinding object with NativeClusterRole reference.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithClusterRole_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "cluster-role-binding", Namespace = "default" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.ClusterRole,
        Name = "admin"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.User,
          Name = "admin-user",
        }
      ]
    };

    // Act
    string fileName = "role-binding-with-cluster-role.yaml";
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
  /// Verifies the generated NativeRoleBinding object with multiple subjects.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "multi-subject-binding", Namespace = "default" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.Role,
        Name = "reader"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.User,
          Name = "user1",
        },
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.Group,
          Name = "readers",
        },
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.ServiceAccount,
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
  /// Verifies the generated NativeRoleBinding object without namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithoutNamespace_ShouldGenerateAValidRoleBinding()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "simple-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.Role,
        Name = "simple-role"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.User,
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has an invalid RoleRef kind.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithInvalidRoleRefKind_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleBindingGenerator();
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "invalid-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = (NativeRoleBindingRoleRefKind)999, // Invalid enum value
        Name = "invalid"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.User,
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
    var model = new NativeRoleBinding
    {
      Metadata = new() { Name = "invalid-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.Role,
        Name = "role"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = (NativeRoleBindingSubjectKind)999, // Invalid enum value
          Name = "invalid",
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
