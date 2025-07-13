using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.GenericSecretGeneratorTests;

/// <summary>
/// Tests for the <see cref="GenericSecretGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated generic Secret object using V1Secret input.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecret_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new GenericSecret("generic-secret")
    {
      Metadata = { Namespace = "default" },
      Type = "Opaque"
    };
    model.Data["key1"] = "value1";
    model.Data["key2"] = "value2";

    // Act
    string fileName = "generic-secret.yaml";
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
  /// Verifies the generated generic Secret object without specifying a type.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretWithoutType_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "generic-secret-no-type",
        NamespaceProperty = "default"
      },
      StringData = new Dictionary<string, string>
      {
        ["key1"] = "value1"
      }
    };

    // Act
    string fileName = "generic-secret-no-type.yaml";
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
  /// Verifies the generated generic Secret object with both Data and StringData.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithGenericSecretWithDataAndStringData_ShouldGenerateAValidGenericSecret()
  {
    // Arrange
    var generator = new GenericSecretGenerator();
    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret",
      Metadata = new V1ObjectMeta
      {
        Name = "generic-secret-mixed",
        NamespaceProperty = "default"
      },
      Type = "Opaque",
      Data = new Dictionary<string, byte[]>
      {
        ["data-key"] = "data-value"u8.ToArray(),
        ["override-key"] = "data-override"u8.ToArray()
      },
      StringData = new Dictionary<string, string>
      {
        ["string-key"] = "string-value",
        ["override-key"] = "string-override"
      }
    };

    // Act
    string fileName = "generic-secret-mixed.yaml";
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
  public async Task GenerateAsync_WithDockerRegistrySecretWithoutName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new GenericSecretGenerator();

    var model = new V1Secret
    {
      ApiVersion = "v1",
      Kind = "Secret"
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }
}
