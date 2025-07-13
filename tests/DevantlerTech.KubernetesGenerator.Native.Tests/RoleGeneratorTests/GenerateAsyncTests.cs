using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.RoleGeneratorTests;

/// <summary>
/// Tests for the <see cref="RoleGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
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
      Rules = [
        new PolicyRule
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
    var model = new Role
    {
      Metadata = new V1ObjectMeta
      {
        Name = "test-role"
      },
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
    var model = new Role
    {
      Metadata = new V1ObjectMeta
      {
        Name = "test-role"
      },
      Rules = [
        new PolicyRule
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
    var model = new Role
    {
      Metadata = new V1ObjectMeta
      {
        Name = "test-role"
      },
      Rules = [
        new PolicyRule
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
