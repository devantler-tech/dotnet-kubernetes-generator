using DevantlerTech.KubernetesGenerator.Core;
using DevantlerTech.KubernetesGenerator.Core.Models;
using DevantlerTech.KubernetesGenerator.Native.Models.ConfigMap;

namespace DevantlerTech.KubernetesGenerator.Native.Tests.ConfigMapGeneratorTests;

/// <summary>
/// Tests for the <see cref="ConfigMapGenerator"/> class.
/// </summary>
internal sealed class GenerateAsyncTests
{
  /// <summary>
  /// Test data for valid ConfigMap generation scenarios.
  /// </summary>
  public static TheoryData<NativeConfigMap, string> ValidConfigMapData =>
    new()
    {
      {
        new NativeConfigMap
        {
          Metadata = new Metadata { Name = "test-config", Namespace = "default" },
          Data = new Dictionary<string, string> { ["key1"] = "value1", ["key2"] = "value2" }
        },
        "config-map-literal.yaml"
      },
      {
        new NativeConfigMap
        {
          Metadata = new Metadata { Name = "test-config-hash" },
          Data = new Dictionary<string, string>
          {
            ["database_url"] = "postgresql://localhost:5432/mydb",
            ["api_key"] = "secret-api-key"
          },
          AppendHash = true
        },
        "config-map-literal-hash.yaml"
      },
      {
        new NativeConfigMap
        {
          Metadata = new Metadata { Name = "test-empty", Namespace = "default" },
          Data = new Dictionary<string, string>()
        },
        "config-map-empty.yaml"
      }
    };

  /// <summary>
  /// Verifies the generated NativeConfigMap object with different configurations.
  /// </summary>
  /// <param name="model">The ConfigMap model to test.</param>
  /// <param name="fileName">The expected output file name.</param>
  /// <returns></returns>
  [Theory]
  [MemberData(nameof(ValidConfigMapData))]
  public async Task GenerateAsync_WithValidConfiguration_ShouldGenerateAValidConfigMap(NativeConfigMap model, string fileName)
  {
    // Arrange
    var generator = new ConfigMapGenerator();

    // Act & Assert
    await GenerateAndVerifyAsync(generator, model, fileName);
  }

  /// <summary>
  /// Verifies that a <see cref="KubernetesGeneratorException"/> is thrown when the NativeConfigMap name is invalid.
  /// </summary>
  /// <param name="name">The ConfigMap name to test.</param>
  /// <returns></returns>
  [Theory]
  [InlineData("")]
  [InlineData(null)]
  public async Task GenerateAsync_WithInvalidConfigMapName_ShouldThrowKubernetesGeneratorException(string? name)
  {
    // Arrange
    var generator = new ConfigMapGenerator();
    var model = new NativeConfigMap
    {
      Metadata = new Metadata { Name = name! },
      Data = new Dictionary<string, string> { ["key"] = "value" }
    };

    // Act & Assert
    _ = await Assert.ThrowsAsync<KubernetesGeneratorException>(() =>
      generator.GenerateAsync(model, Path.GetTempFileName()));
  }

  /// <summary>
  /// Helper method to generate a file and verify its content.
  /// </summary>
  /// <typeparam name="TModel">The model type.</typeparam>
  /// <typeparam name="TGenerator">The generator type.</typeparam>
  /// <param name="generator">The generator instance.</param>
  /// <param name="model">The model to generate.</param>
  /// <param name="fileName">The file name for verification.</param>
  /// <returns>A task representing the async operation.</returns>
  static async Task GenerateAndVerifyAsync<TModel, TGenerator>(
    TGenerator generator,
    TModel model,
    string fileName)
    where TGenerator : IKubernetesGenerator<TModel>
  {
    string outputPath = Path.Combine(Path.GetTempPath(), fileName);
    if (File.Exists(outputPath))
      File.Delete(outputPath);

    await generator.GenerateAsync(model, outputPath).ConfigureAwait(false);
    string fileContent = await File.ReadAllTextAsync(outputPath).ConfigureAwait(false);

    _ = await Verify(fileContent, extension: "yaml").UseFileName(fileName);

    File.Delete(outputPath);
  }
}
