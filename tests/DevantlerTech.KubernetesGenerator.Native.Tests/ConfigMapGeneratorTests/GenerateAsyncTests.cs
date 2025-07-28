using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Native.Models;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;


/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated ConfigMap object using kubectl create configmap with literal data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralData_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-config",
        Namespace = "default"
      },
      Data = new Dictionary<string, string>
      {
        ["key1"] = "value1",
        ["key2"] = "value2"
      }
    };

    // Act
    string fileName = "config-map-literal.yaml";
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
  /// Verifies the generated ConfigMap object using kubectl create configmap with literal data and append hash.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralDataAndAppendHash_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-config-hash"
      },
      Data = new Dictionary<string, string>
      {
        ["database_url"] = "postgresql://localhost:5432/mydb",
        ["api_key"] = "secret-api-key"
      },
      AppendHash = true
    };

    // Act
    string fileName = "config-map-literal-hash.yaml";
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the ConfigMap name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyConfigMapName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = ""
      },
      Data = new Dictionary<string, string>
      {
        ["key"] = "value"
      }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() => generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Verifies the generated ConfigMap object with no data creates an empty ConfigMap.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNoData_ShouldGenerateEmptyConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-empty",
        Namespace = "default"
      },
      Data = new Dictionary<string, string>()
    };

    // Act
    string fileName = "config-map-empty.yaml";
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
  /// Verifies the generated ConfigMap object with null data creates an empty ConfigMap.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithNullData_ShouldGenerateEmptyConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new ConfigMap
    {
      Metadata = new Metadata
      {
        Name = "test-null",
        Namespace = "default"
      },
      Data = null
    };

    // Act
    string fileName = "config-map-null.yaml";
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
}
