using DevantlerTech.KubernetesGenerator.Core;
using k8s.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ServiceAccountGeneratorTests;

/// <summary>
/// Tests for the <see cref="ServiceAccountGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ServiceAccount object with basic properties.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithBasicProperties_ShouldGenerateAValidServiceAccount()
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount",
      Metadata = new V1ObjectMeta
      {
        Name = "self-subject-review",
        NamespaceProperty = "default"
      }
    };

    // Act
    string fileName = "service-account.yaml";
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
  /// Verifies the generated ServiceAccount object with only name (no namespace).
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNameOnly_ShouldGenerateAValidServiceAccount()
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount",
      Metadata = new V1ObjectMeta
      {
        Name = "simple-service-account"
      }
    };

    // Act
    string fileName = "service-account-no-namespace.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the model does not have a name set.
  /// </summary>
  [Fact]
  public async Task GenerateAsync_WithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ServiceAccountGenerator();
    var model = new V1ServiceAccount
    {
      ApiVersion = "v1",
      Kind = "ServiceAccount"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName())).ConfigureAwait(false);
  }
}
