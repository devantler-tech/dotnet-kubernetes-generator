using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.ClusterRoleBinding;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ClusterRoleBindingGeneratorTests;

/// <summary>
/// Tests for the <see cref="ClusterRoleBindingGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeClusterRoleBinding object with all properties set.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithAllPropertiesSet_ShouldGenerateAValidClusterRoleBinding()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new NativeClusterRoleBinding
    {
      Metadata = new Metadata { Name = "cluster-role-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.ClusterRole,
        Name = "cluster-admin"
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
  /// Verifies the generated NativeClusterRoleBinding object with multiple subjects.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleSubjects_ShouldGenerateAValidClusterRoleBinding()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new NativeClusterRoleBinding
    {
      Metadata = new Metadata { Name = "multi-subject-cluster-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.ClusterRole,
        Name = "view"
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
          Name = "viewers",
        },
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.ServiceAccount,
          Name = "viewer-sa",
          Namespace = "kube-system"
        }
      ]
    };

    // Act
    string fileName = "multi-subject-cluster-binding.yaml";
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
  /// Verifies the generated NativeClusterRoleBinding object with service account using default namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithServiceAccountDefaultNamespace_ShouldGenerateAValidClusterRoleBinding()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new NativeClusterRoleBinding
    {
      Metadata = new Metadata { Name = "sa-default-cluster-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.ClusterRole,
        Name = "view"
      },
      Subjects =
      [
        new NativeRoleBindingSubject
        {
          Kind = NativeRoleBindingSubjectKind.ServiceAccount,
          Name = "default-sa"
          // No namespace specified, should default to 'default'
        }
      ]
    };

    // Act
    string fileName = "sa-default-cluster-binding.yaml";
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
  public async Task GenerateAsync_WithRoleRefInsteadOfClusterRole_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ClusterRoleBindingGenerator();
    var model = new NativeClusterRoleBinding
    {
      Metadata = new Metadata { Name = "invalid-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.Role, // Invalid - NativeClusterRoleBinding requires NativeClusterRole
        Name = "role"
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
    var generator = new ClusterRoleBindingGenerator();
    var model = new NativeClusterRoleBinding
    {
      Metadata = new Metadata { Name = "invalid-binding" },
      RoleRef = new NativeRoleBindingRoleRef
      {
        Kind = NativeRoleBindingRoleRefKind.ClusterRole,
        Name = "cluster-admin"
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
