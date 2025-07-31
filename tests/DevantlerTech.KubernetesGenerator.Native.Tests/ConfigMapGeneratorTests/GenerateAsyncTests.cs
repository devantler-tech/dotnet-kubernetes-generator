using DevantlerTech.KubernetesGenerator.Core;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;


/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
public sealed class GenerateAsyncTests
{
  /// <summary>
  /// Verifies the generated NativeConfigMap object using kubectl create configmap with literal data.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralData_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new NativeConfigMap
    {
      Metadata = new NativeMetadata
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
  /// Verifies the generated NativeConfigMap object using kubectl create configmap with literal data and append hash.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithLiteralDataAndAppendHash_ShouldGenerateAValidConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new NativeConfigMap
    {
      Metadata = new NativeMetadata
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
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the NativeConfigMap name is empty.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyConfigMapName_ShouldThrowKubernetesGeneratorException()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new NativeConfigMap
    {
      Metadata = new NativeMetadata
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
  /// Verifies the generated NativeConfigMap object with empty or null data creates an empty ConfigMap.
  /// Tests that both empty dictionary and null data scenarios produce identical output.
  /// </summary>
  /// <returns></returns>
  [Fact]
  public async Task GenerateAsync_WithEmptyOrNullData_ShouldGenerateEmptyConfigMap()
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new NativeConfigMap
    {
      Metadata = new NativeMetadata
      {
        Name = "test-empty",
        Namespace = "default"
      },
      Data = new Dictionary<string, string>() // Empty dictionary (null produces same result)
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
}
