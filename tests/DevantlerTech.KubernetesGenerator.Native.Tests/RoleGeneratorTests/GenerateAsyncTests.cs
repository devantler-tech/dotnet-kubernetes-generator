using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated Role object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("pod-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list", "watch"],
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
  /// Verifies the generated Role object with namespace.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNamespace_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("pod-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list", "watch"],
          Resources = ["pods"]
        }
      ]
    };
    model.Metadata.Namespace = "default";

    // Act
    string fileName = "role-with-namespace.yaml";
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
  /// Verifies the generated Role object with API groups.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithApiGroups_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("deployment-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list"],
          Resources = ["deployments"],
          ApiGroups = ["apps"]
        }
      ]
    };

    // Act
    string fileName = "role-with-api-groups.yaml";
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
  /// Verifies the generated Role object with mixed API groups (core and apps).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMixedApiGroups_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("mixed-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list"],
          Resources = ["pods", "deployments"],
          ApiGroups = ["", "apps"]
        }
      ]
    };

    // Act
    string fileName = "role-with-mixed-api-groups.yaml";
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
  /// Verifies the generated Role object with resource names.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithResourceNames_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("specific-pod-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "update"],
          Resources = ["pods"],
          ResourceNames = ["my-pod", "another-pod"]
        }
      ]
    };

    // Act
    string fileName = "role-with-resource-names.yaml";
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
  /// Verifies the generated Role object with multiple resources.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleResources_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("multi-resource-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list", "watch"],
          Resources = ["pods", "services", "configmaps"]
        }
      ]
    };

    // Act
    string fileName = "role-with-multiple-resources.yaml";
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
  /// Verifies the generated Role object with multiple rules combined.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithMultipleRules_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("multi-rule-reader")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list"],
          Resources = ["pods"]
        },
        new Rule
        {
          Verbs = ["watch"],
          Resources = ["services"]
        }
      ]
    };

    // Act
    string fileName = "role-with-multiple-rules.yaml";
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
  /// Verifies the generated Role object with comprehensive configuration.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithComprehensiveConfiguration_ShouldGenerateAValidRole()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("comprehensive-role")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list", "watch"],
          Resources = ["pods", "services"],
          ApiGroups = [""]
        },
        new Rule
        {
          Verbs = ["create", "update", "patch"],
          Resources = ["deployments"],
          ApiGroups = ["apps"],
          ResourceNames = ["my-deployment"]
        }
      ]
    };
    model.Metadata.Namespace = "production";

    // Act
    string fileName = "role-comprehensive.yaml";
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
    var model = new Role("")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get"],
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
    var model = new Role("test-role")
    {
      Rules = []
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has rules without verbs.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutVerbs_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("test-role")
    {
      Rules = [
        new Rule
        {
          Verbs = [],
          Resources = ["pods"]
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model has rules without resources.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutResources_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new RoleGenerator();
    var model = new Role("test-role")
    {
      Rules = [
        new Rule
        {
          Verbs = ["get", "list"],
          Resources = []
        }
      ]
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
