using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleGeneratorTests;


/// <summary>
/// Tests for the <see cref="RoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Role object with basic permissions.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicRole_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = "pod-reader",
        Namespace = "default"
      },
      Rules =
      [
        new RoleRule
        {
          Verbs = [RoleVerb.Get, RoleVerb.List, RoleVerb.Watch],
          Resources = ["pods"]
        }
      ]
    };

    // Act
    string fileName = "role.yaml";
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
  /// Verifies the generated Role object with complex permissions including API groups and resource names.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComplexRole_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = "complex-role",
        Namespace = "test-namespace"
      },
      Rules =
      [
        new RoleRule
        {
          Verbs = [RoleVerb.Get, RoleVerb.Create],
          Resources = ["pods", "services"],
          ApiGroups = ["", "apps"],
          ResourceNames = ["my-pod", "my-service"]
        }
      ]
    };

    // Act
    string fileName = "complex-role.yaml";
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
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = ""  // Empty name should trigger validation
      },
      Rules =
      [
        new RoleRule
        {
          Verbs = [RoleVerb.Get],
          Resources = ["pods"]
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have any rules.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutRules_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = "test-role"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when a rule has no verbs.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithRuleWithoutVerbs_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = "test-role"
      },
      Rules =
      [
        new RoleRule
        {
          Verbs = [], // Empty verbs should trigger validation
          Resources = ["pods"]
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when a rule has no resources.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithRuleWithoutResources_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role
    {
      Metadata = new Metadata
      {
        Name = "test-role"
      },
      Rules =
      [
        new RoleRule
        {
          Verbs = [RoleVerb.Get]
          // Resources missing - should trigger validation
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
